using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DeleteFromHost
{
    public class RemoveMikrotikHostHandler : IRequestHandler<RemoveMikrotikHostCommand, bool>
    {
        private readonly IMikrotikService _mikrotikService;
        public RemoveMikrotikHostHandler(IMikrotikService service) => _mikrotikService = service;

        public async Task<bool> Handle(RemoveMikrotikHostCommand request, CancellationToken ct)
        {
            return await _mikrotikService.RemoveHostByMacAsync(request.MacAddress);
        }
    }
}
