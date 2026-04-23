using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.Mikrotik.Queries.GetMyDepartmentConsumption
{
    public class GetMyDepartmentConsumptionHandler : IRequestHandler<GetMyDepartmentConsumptionQuery, DetailedDepartmentConsumptionResponse>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IDepartmentService _departmentService; // استخدام الخدمة المتاحة لديك

        public GetMyDepartmentConsumptionHandler(IMikrotikService mikrotikService, IDepartmentService departmentService)
        {
            _mikrotikService = mikrotikService;
            _departmentService = departmentService;
        }

        public async Task<DetailedDepartmentConsumptionResponse> Handle(GetMyDepartmentConsumptionQuery request, CancellationToken cancellationToken)
        {
            // 1. التحقق من وجود القسم في النظام
            var department = await _departmentService.GetByIdAsync(request.DepartmentId);

            if (department == null)
                throw new Exception("Department not found");

            // 2. طلب بيانات الاستهلاك من خدمة الميكروتيك
            // نرسل اسم القسم للفلترة
            return await _mikrotikService.GetUsageByDepartmentNameAsync(department.Name);
        }
    }
}

