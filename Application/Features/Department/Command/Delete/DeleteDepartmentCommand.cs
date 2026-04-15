 using Application.DTOs.Response;
using MediatR;

namespace Application.Features.Department.Command.Delete
{
    public record DeleteDepartmentCommand(int Id) : IRequest<Unit>;
}