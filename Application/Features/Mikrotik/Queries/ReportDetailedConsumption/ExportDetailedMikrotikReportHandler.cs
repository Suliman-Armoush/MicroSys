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

        public ExportDetailedMikrotikReportHandler(IMikrotikService mikrotikService, IExcelService excelService)
        {
            _mikrotikService = mikrotikService;
            _excelService = excelService;
        }

        public async Task<byte[]> Handle(ExportDetailedMikrotikReportQuery request, CancellationToken cancellationToken)
        {
            var usersDto = await _mikrotikService.GetAllUsersAsync();

            var allDetailedData = usersDto
                .GroupBy(u => (u.Comment ?? "undefined").TrimStart('@', '#').Split(' ')[0])
                .Select(g => new DetailedDepartmentConsumptionResponse
                {
                    DepartmentName = g.Key,
                    TotalConsumptionGB = Math.Round(g.Sum(x => x.BytesInRaw + x.BytesOutRaw) / Math.Pow(1024, 3), 2),
                    Users = g.Select(u => new UserConsumptionDetail
                    {
                        UserName = u.Comment, // تم التعديل هنا لحل خطأ Name
                        UsageGB = Math.Round((u.BytesInRaw + u.BytesOutRaw) / Math.Pow(1024, 3), 2)
                    }).OrderByDescending(u => u.UsageGB).ToList(),

                    // تحديد النوع للتقسيم إلى شيتات (حل خطأ Type)
                    Type = (g.First().Comment ?? "").StartsWith("@") ? "AT" :
                           (g.First().Comment ?? "").StartsWith("#") ? "HASH" : "NORMAL"
                }).ToList();

            // تقسيم البيانات لـ 3 قوائم لإرسالها للسيرفس
            var atData = allDetailedData.Where(x => x.Type == "AT").ToList();
            var hashData = allDetailedData.Where(x => x.Type == "HASH").ToList();
            var normalData = allDetailedData.Where(x => x.Type == "NORMAL").ToList();

            return _excelService.GenerateDetailedExcelReport(atData, hashData, normalData);
        }
    }
}
