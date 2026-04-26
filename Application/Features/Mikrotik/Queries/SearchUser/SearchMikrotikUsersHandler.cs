using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.SearchUser
{
    public class SearchMikrotikUsersHandler : IRequestHandler<SearchMikrotikUsersQuery, List<MikrotikUserInformationResponse>>
    {
        private readonly IMikrotikService _mikrotikService;

        public SearchMikrotikUsersHandler(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
        }

        public async Task<List<MikrotikUserInformationResponse>> Handle(SearchMikrotikUsersQuery request, CancellationToken cancellationToken)
        {
            return await _mikrotikService.SearchUsersAsync(request.Term);
        }
    }
}
