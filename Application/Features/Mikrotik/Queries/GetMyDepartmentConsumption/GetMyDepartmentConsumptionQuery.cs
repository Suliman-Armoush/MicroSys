using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetMyDepartmentConsumption
{
    public class GetMyDepartmentConsumptionQuery : IRequest<DetailedDepartmentConsumptionResponse>
    {
        public int DepartmentId { get; set; }
        public GetMyDepartmentConsumptionQuery(int departmentId) => DepartmentId = departmentId;
    }
}

