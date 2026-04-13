using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.Features.Department.Command.Create
{
    public record CreateDepartmentCommand(DepartmentRequestDto DepartmentDto) : IRequest<DepartmentResponseDto>;
}