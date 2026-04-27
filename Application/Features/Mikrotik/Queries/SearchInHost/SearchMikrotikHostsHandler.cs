using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.SearchInHost
{
    public class SearchMikrotikHostsHandler : IRequestHandler<SearchMikrotikHostsQuery, List<MikrotikHostResponse>>
    {
        private readonly IMikrotikService _mikrotikService;
        public SearchMikrotikHostsHandler(IMikrotikService service) => _mikrotikService = service;

        public async Task<List<MikrotikHostResponse>> Handle(SearchMikrotikHostsQuery request, CancellationToken ct)
        {
            return await _mikrotikService.SearchHostsAsync(request.Term);
        }
    }
}
