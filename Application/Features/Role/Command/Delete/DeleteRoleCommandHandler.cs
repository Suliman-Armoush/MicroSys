using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Role.Command.Delete
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleService.GetByIdAsync(request.Id);

            if (role == null)
                throw new KeyNotFoundException("Role not found.");

            await _roleService.DeleteAsync(role);
            return Unit.Value;
        }
    }
}
