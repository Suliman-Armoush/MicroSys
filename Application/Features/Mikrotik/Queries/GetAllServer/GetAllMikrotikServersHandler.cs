using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetAllServer
{
    public class GetAllMikrotikServersHandler : IRequestHandler<GetAllMikrotikServersQuery, List<MikrotikServerResponse>>
    {
        private readonly IMikrotikService _mikrotikService;
        public GetAllMikrotikServersHandler(IMikrotikService service) => _mikrotikService = service;

        public async Task<List<MikrotikServerResponse>> Handle(GetAllMikrotikServersQuery request, CancellationToken ct)
        {
            return await _mikrotikService.GetAllServersAsync();
        }
    }
}
