using Application.DTOs.Response;
using MediatR;

namespace Application.Features.Department.Queries.GetById
{
    public class GetDepartmentByIdQuery : IRequest<DepartmentResponseDto>
    {
        public int Id { get; }

        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}