using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.DepartmentConsumption
{
    public class GetDepartmentsConsumptionQuery : IRequest<List<DepartmentConsumptionResponse>>
    {
    }
}
