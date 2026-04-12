using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.Features.Department.Command.Create
{
    public class CreateDepartmentCommand : IRequest<DepartmentResponseDto>
    {
        public DepartmentRequestDto DepartmentDto { get; set; }

        public CreateDepartmentCommand(DepartmentRequestDto dto)
        {
            DepartmentDto = dto;
        }
    }
}