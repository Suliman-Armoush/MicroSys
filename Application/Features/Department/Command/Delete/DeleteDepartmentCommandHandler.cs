using Application.Interfaces;
using MediatR;

namespace Application.Features.Department.Command.Delete
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IDepartmentService _departmentService;

        public DeleteDepartmentCommandHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetByIdAsync(request.Id);

            if (department == null) return false;

            await _departmentService.DeleteAsync(department);

            return true;
        }
    }
}