using Application.DTOs.Response;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Department.Queries.GetAll
{
    public class GetAllDepartmentsQuery : IRequest<List<DepartmentResponseDto>>
    {
    }
}