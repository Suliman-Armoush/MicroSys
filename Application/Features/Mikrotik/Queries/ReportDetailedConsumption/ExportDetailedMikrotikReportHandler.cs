using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.ReportDetailedConsumption
{


    public class ExportDetailedMikrotikReportHandler : IRequestHandler<ExportDetailedMikrotikReportQuery, byte[]>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IExcelService _excelService;
        private readonly IDepartmentService _departmentService;

        // 2. أضف الخدمة في المنشئ (Constructor)
        public ExportDetailedMikrotikReportHandler(IMikrotikService mikrotikService, IExcelService excelService, IDepartmentService departmentService)
        {
            _mikrotikService = mikrotikService;
            _excelService = excelService;
            _departmentService = departmentService;
        }

        public async Task<byte[]> Handle(ExportDetailedMikrotikReportQuery request, CancellationToken cancellationToken)
        {
            // 1. جلب البيانات
            var usersDto = await _mikrotikService.GetAllUsersAsync();
            var dbDepartments = await _departmentService.GetAllAsync();

            var allDetailedData = usersDto
                .Where(u => !string.IsNullOrEmpty(u.Comment))
                .GroupBy(u => {
                    // منطق استخراج اسم القسم: إزالة الرموز ثم أخذ أول كلمة أو النص قبل الفراغ/الشرطة
                    var cleanComment = u.Comment.TrimStart('@', '#', ' ').Trim();
                    // نأخذ النص قبل أول فراغ أو أول شرطة أو أول نقطة
                    char[] delimiters = { ' ', '-', '.', '_' };
                    return cleanComment.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0].Trim().ToLower();
                })
                .Select(g => {
                    var deptKey = g.Key; // اسم القسم المستخرج (مثلاً: it أو saray)

                    // البحث عن نوع القسم في قاعدة البيانات
                    var deptInfo = dbDepartments.FirstOrDefault(d =>
                        d.Name.Trim().ToLower() == deptKey);

                    return new DetailedDepartmentConsumptionResponse
                    {
                        DepartmentName = deptKey.ToUpper(), // عرض اسم القسم بأحرف كبيرة للتنسيق
                        TotalConsumptionGB = Math.Round(g.Sum(x => x.BytesInRaw + x.BytesOutRaw) / Math.Pow(1024, 3), 2),
                        Users = g.Select(u => new UserConsumptionDetail
                        {
                            // حذف اسم القسم من اسم المستخدم لتنظيف العرض
                            // إذا كان الكومنت "saray - ahmad" سيصبح "ahmad"
                            UserName = CleanUserName(u.Comment, deptKey),
                            UsageGB = Math.Round((u.BytesInRaw + u.BytesOutRaw) / Math.Pow(1024, 3), 2)
                        }).OrderByDescending(u => u.UsageGB).ToList(),

                        Type = deptInfo?.Type.ToString() ?? "Unknown"
                    };
                }).ToList();

            // 2. التوزيع على الشيتات
            var serviceData = allDetailedData.Where(x => x.Type == "Service").ToList();
            var tcShopsData = allDetailedData.Where(x => x.Type == "TcShops").ToList();
            var shopsData = allDetailedData.Where(x => x.Type == "Shops").ToList();

            return _excelService.GenerateDetailedExcelReport(serviceData, tcShopsData, shopsData);
        }

        // دالة مساعدة لتنظيف اسم المستخدم وحذف اسم القسم منه
        private string CleanUserName(string fullComment, string deptName)
        {
            if (string.IsNullOrEmpty(fullComment)) return "Unknown";

            // إزالة الرموز والاسم
            var cleaned = fullComment.Replace("@", "").Replace("#", "").Trim();

            // إذا كان يبدأ باسم القسم، قم بحذفه وما يليه من رموز (مثل - أو فراغ)
            if (cleaned.ToLower().StartsWith(deptName.ToLower()))
            {
                cleaned = cleaned.Substring(deptName.Length).TrimStart(' ', '-', '.', '_', ':');
            }

            return string.IsNullOrEmpty(cleaned) ? fullComment : cleaned;
        }
    }
}
