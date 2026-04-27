using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetAllHosts
{
    public class GetAllMikrotikHostsHandler : IRequestHandler<GetAllMikrotikHostsQuery, List<MikrotikHostResponse>>
    {
        private readonly IMikrotikService _mikrotikService;

        public GetAllMikrotikHostsHandler(IMikrotikService service)
        {
            _mikrotikService = service;
        }

        public async Task<List<MikrotikHostResponse>> Handle(GetAllMikrotikHostsQuery request, CancellationToken ct)
        {
            return await _mikrotikService.GetAllHostsAsync();
        }
    }
}
