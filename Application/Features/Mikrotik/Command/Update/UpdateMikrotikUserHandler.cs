using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Mikrotik.Command.Update
{
    public class UpdateMikrotikUserHandler : IRequestHandler<UpdateMikrotikUserCommand, MikrotikUserInformationResponse>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IDepartmentService _departmentService;

        public UpdateMikrotikUserHandler(IMikrotikService mikrotikService, IDepartmentService departmentService)
        {
            _mikrotikService = mikrotikService;
            _departmentService = departmentService;
        }

        public async Task<MikrotikUserInformationResponse> Handle(UpdateMikrotikUserCommand request, CancellationToken cancellationToken)
        {
            // 1. جلب بيانات المستخدم الحالية من الميكروتيك (ضروري جداً لتجنب الـ Null والحفاظ على القديم)
            var currentUser = await _mikrotikService.GetUserByNameAsync(request.CurrentUsername);
            if (currentUser == null) throw new KeyNotFoundException("User not found on Mikrotik.");

            // 2. تفكيك الكومنت القديم (مثال: "@IT - Homam") للحفاظ على الأجزاء التي لن تتغير
            string oldDeptName = "";
            string oldDetails = "";
            if (!string.IsNullOrEmpty(currentUser.Comment) && currentUser.Comment.Contains("-"))
            {
                var parts = currentUser.Comment.Split('-');
                oldDeptName = parts[0].Replace("@", "").Trim();
                oldDetails = parts[1].Trim();
            }
            else
            {
                // إذا الكومنت قديم أو بتنسيق مختلف، نعتبر التفاصيل هي اسم المستخدم الحالي
                oldDetails = currentUser.Username;
            }

            // 3. تحديد اسم القسم الجديد أو الحفاظ على القديم
            string finalDeptName = oldDeptName;
            if (request.DepartmentId.HasValue && request.DepartmentId > 0)
            {
                var department = await _departmentService.GetByIdAsync(request.DepartmentId.Value);
                if (department != null)
                {
                    finalDeptName = department.Name;
                }
            }

            // 4. تحديد التفاصيل (UserDetails) الجديدة أو الحفاظ على القديمة
            string finalDetails = !string.IsNullOrEmpty(request.UserDetails) ? request.UserDetails : oldDetails;

            // 5. بناء الكومنت النهائي بنفس تنسيق الكريت (QIMS Standard)
            string finalComment = $"@{finalDeptName} - {finalDetails}";

            // 6. تحويل اللمت لـ Bytes دقيقة (1024 * 1024 * 1024) عشان تظهر 5G بالوين بوكس
            long? limitBytes = null;
            if (request.LimitGB.HasValue && request.LimitGB.Value > 0)
            {
                // استخدام رقم دقيق لضمان ظهور الرمز G في الميكروتيك
                limitBytes = (long)(request.LimitGB.Value * 1073741824);
            }

            // 7. تجهيز الريكويست الموجه للسيرفس
            var serviceRequest = new UpdateMikrotikUserRequest
            {
                NewUsername = request.NewUsername,
                Password = request.Password,
                Profile = request.Profile,
                Server = request.Server, // ستقوم السيرفس بفحصه إذا كان Null لن تلمس القديم
                Comment = finalComment,
                LimitBytes = limitBytes
            };

            // 8. استدعاء السيرفس مع تمرير الاسم الحالي (المعرف) والريكويست الجديد
            return await _mikrotikService.UpdateUserAsync(serviceRequest, request.CurrentUsername);
        }
    }
}
