using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.Features.Department.Command.Update
{
    public class UpdateDepartmentCommand : IRequest<DepartmentResponseDto>
    {
        public int Id { get; }
        public DepartmentRequestDto DepartmentDto { get; }

        public UpdateDepartmentCommand(int id, DepartmentRequestDto dto)
        {
            Id = id;
            DepartmentDto = dto;
        }
    }
}