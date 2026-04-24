using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mikrotik.Queries.ReportTotalConsumption
{


    public class ExportMikrotikExcelHandler : IRequestHandler<ExportMikrotikExcelQuery, byte[]>
    {
        private readonly IMikrotikService _mikrotikService;
        private readonly IExcelService _excelService;
        private readonly IDepartmentService _departmentService; // حقن السيرفس هنا

        public ExportMikrotikExcelHandler(IMikrotikService mikrotikService, IExcelService excelService, IDepartmentService departmentService)
        {
            _mikrotikService = mikrotikService;
            _excelService = excelService;
            _departmentService = departmentService;
        }

        public async Task<byte[]> Handle(ExportMikrotikExcelQuery request, CancellationToken cancellationToken)
        {
            var usersDto = await _mikrotikService.GetAllUsersAsync();
            var dbDepartments = await _departmentService.GetAllAsync();

            var allProcessedData = usersDto
                .Where(u => !string.IsNullOrEmpty(u.Comment))
                .GroupBy(u =>
                {
                    var clean = u.Comment.TrimStart('@', '#', ' ').Trim();
                    char[] delimiters = { ' ', '-', '.', '_' };
                    return clean.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0].Trim().ToLower();
                })
                // الفلترة لضمان ظهور الأقسام المعرفة لديك فقط وحذف العشوائي مثل "MotoBurger"
                .Where(g => dbDepartments.Any(d => d.Name.Trim().ToLower() == g.Key))
                .Select(g =>
                {
                    var deptKey = g.Key;
                    var deptInfo = dbDepartments.First(d => d.Name.Trim().ToLower() == deptKey);

                    return new DepartmentConsumptionResponse
                    {
                        DepartmentName = deptKey.ToUpper(),
                        TotalConsumptionGB = Math.Round(g.Sum(x => x.BytesInRaw + x.BytesOutRaw) / Math.Pow(1024, 3), 2),
                        Type = deptInfo.Type.ToString()
                    };
                }).ToList();

            // تقسيم البيانات المفلترة بناءً على النوع الفعلي من قاعدة البيانات
            var serviceData = allProcessedData.Where(x => x.Type == "Service").ToList();
            var tcShopsData = allProcessedData.Where(x => x.Type == "TcShops").ToList();
            var shopsData = allProcessedData.Where(x => x.Type == "Shops").ToList();

            return _excelService.GenerateMikrotikReport(serviceData, tcShopsData, shopsData);
        }
    }
}
