using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.EnableUser
{
    public class EnableMikrotikUserHandler : IRequestHandler<EnableMikrotikUserCommand, bool>
    {
        private readonly IMikrotikService _mikrotikService;

        public EnableMikrotikUserHandler(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
        }

        public async Task<bool> Handle(EnableMikrotikUserCommand request, CancellationToken cancellationToken)
        {
            return await _mikrotikService.UpdateUserStatusAsync(request.Username, isDisabled: false);
        }
    }
}
