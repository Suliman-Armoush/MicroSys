using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DisableUser
{
    public class DisableMikrotikUserHandler : IRequestHandler<DisableMikrotikUserCommand, bool>
    {
        private readonly IMikrotikService _service;
        public DisableMikrotikUserHandler(IMikrotikService service) => _service = service;

        public async Task<bool> Handle(DisableMikrotikUserCommand request, CancellationToken ct)
        {
            return await _service.UpdateUserStatusAsync(request.Username, isDisabled: true);
        }
    }
}
