using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.DepartmentConsumption
{
    public class GetDepartmentsConsumptionHandler : IRequestHandler<GetDepartmentsConsumptionQuery, List<DepartmentConsumptionResponse>>
    {
        private readonly IMikrotikService _mikrotikService;

        public GetDepartmentsConsumptionHandler(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
        }

            public async Task<List<DepartmentConsumptionResponse>> Handle(GetDepartmentsConsumptionQuery request, CancellationToken cancellationToken)
            {
                var usersDto = await _mikrotikService.GetAllUsersAsync();

                var result = usersDto
                    .Select(u => new
                    {
                        DeptName = (u.Comment ?? "undefined\r\n")
                                    .TrimStart('@', '#')
                                    .Split(' ')[0],
                        In = u.BytesInRaw,
                        Out = u.BytesOutRaw
                    })
                    .GroupBy(x => x.DeptName)
                    .Select(g => new DepartmentConsumptionResponse
                    {
                        DepartmentName = g.Key,
                    
                        TotalConsumptionGB = Math.Round(g.Sum(x => x.In + x.Out) / Math.Pow(1024, 3), 2),
                        ActiveUsersCount = g.Count()
                    })
                    .OrderByDescending(r => r.TotalConsumptionGB)
                    .ToList();

                return result;
            }
    }
}
