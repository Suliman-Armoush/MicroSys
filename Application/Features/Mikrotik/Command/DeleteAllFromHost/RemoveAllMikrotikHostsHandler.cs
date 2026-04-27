using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DeleteAllFromHost
{
    public class RemoveAllMikrotikHostsHandler : IRequestHandler<RemoveAllMikrotikHostsCommand, bool>
    {
        private readonly IMikrotikService _service;
        public RemoveAllMikrotikHostsHandler(IMikrotikService service) => _service = service;

        public async Task<bool> Handle(RemoveAllMikrotikHostsCommand request, CancellationToken ct)
        {
            return await _service.RemoveAllHostsAsync();
        }
    }
}
