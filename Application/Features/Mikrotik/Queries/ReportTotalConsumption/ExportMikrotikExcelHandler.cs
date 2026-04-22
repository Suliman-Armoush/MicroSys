using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.ReportTotalConsumption
{
    public class ExportMikrotikExcelHandler : IRequestHandler<ExportMikrotikExcelQuery, byte[]>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IExcelService _excelService;

        public ExportMikrotikExcelHandler(IMikrotikService mikrotikService, IExcelService excelService)
        {
            _mikrotikService = mikrotikService;
            _excelService = excelService;
        }

        public async Task<byte[]> Handle(ExportMikrotikExcelQuery request, CancellationToken cancellationToken)
        {
            var usersDto = await _mikrotikService.GetAllUsersAsync();

            // دالة المعالجة المعدلة: تركز فقط على الاسم والإجمالي بالجيجابايت
            List<DepartmentConsumptionResponse> ProcessData(IEnumerable<MikrotikUserResponse> source)
            {
                return source
                    .Select(u => new {
                        // تنظيف الاسم (أخذ الكلمة الأولى بعد حذف الرموز)
                        DeptName = (u.Comment ?? "undefined").TrimStart('@', '#').Split(' ')[0],
                        TotalBytes = u.BytesInRaw + u.BytesOutRaw
                    })
                    .GroupBy(x => x.DeptName)
                    .Select(g => new DepartmentConsumptionResponse
                    {
                        DepartmentName = g.Key,
                        // حساب الإجمالي بالجيجابايت والتقريب لرقمين
                        TotalConsumptionGB = Math.Round(g.Sum(x => x.TotalBytes) / Math.Pow(1024, 3), 2)
                    })
                    .OrderByDescending(r => r.TotalConsumptionGB)
                    .ToList();
            }

            // تقسيم البيانات بناءً على الرمز الأول
            var atRaw = usersDto.Where(u => (u.Comment ?? "").StartsWith("@"));
            var hashRaw = usersDto.Where(u => (u.Comment ?? "").StartsWith("#"));
            var normalRaw = usersDto.Where(u => !(u.Comment ?? "").StartsWith("@") && !(u.Comment ?? "").StartsWith("#"));

            // المعالجة
            var atData = ProcessData(atRaw);
            var hashData = ProcessData(hashRaw);
            var normalData = ProcessData(normalRaw);

            // إرسال البيانات المجهزة لخدمة الإكسل
            return _excelService.GenerateMikrotikReport(atData, hashData, normalData);
        }
    }
}
