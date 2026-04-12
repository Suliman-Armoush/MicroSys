using MediatR;

namespace Application.Features.Department.Command.Delete
{
    public class DeleteDepartmentCommand : IRequest<bool> 
    {
        public int Id { get; }

        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }
}