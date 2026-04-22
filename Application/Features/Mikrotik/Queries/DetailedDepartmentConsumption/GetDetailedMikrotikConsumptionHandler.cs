using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.DetailedDepartmentConsumption
{
    public class GetDetailedMikrotikConsumptionHandler : IRequestHandler<GetDetailedMikrotikConsumptionQuery, List<DetailedDepartmentConsumptionResponse>>
    {
        private readonly IMikrotikService _mikrotikService;

        public GetDetailedMikrotikConsumptionHandler(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
        }

        public async Task<List<DetailedDepartmentConsumptionResponse>> Handle(GetDetailedMikrotikConsumptionQuery request, CancellationToken cancellationToken)
        {
            // 1. جلب بيانات اليوزرات من الميكروتيك
            var usersDto = await _mikrotikService.GetAllUsersAsync();

            // 2. تجميع البيانات وحساب الاستهلاك لكل قسم ولكل يوزر
            var result = usersDto
                .GroupBy(u => (u.Comment ?? "undefined").TrimStart('@', '#').Split(' ')[0])
                .Select(g => new DetailedDepartmentConsumptionResponse
                {
                    DepartmentName = g.Key,
                    // إجمالي استهلاك القسم كاملاً بالجيجابايت
                    TotalConsumptionGB = Math.Round(g.Sum(x => x.BytesInRaw + x.BytesOutRaw) / Math.Pow(1024, 3), 2),

                    // ملء قائمة اليوزرات التابعين لهذا القسم (الاستهلاك الفردي لكل يوزر)
                    Users = g.Select(u => new UserConsumptionDetail
                    {
                        UserName = u.Username,
                        UsageGB = Math.Round((u.BytesInRaw + u.BytesOutRaw) / Math.Pow(1024, 3), 2)
                    })
                    .OrderByDescending(u => u.UsageGB) // ترتيب اليوزرات داخل القسم من الأكثر استهلاكاً
                    .ToList()
                })
                .OrderByDescending(r => r.TotalConsumptionGB) // ترتيب الأقسام نفسها حسب الاستهلاك الكلي
                .ToList();

            return result;
        }
    }
}
