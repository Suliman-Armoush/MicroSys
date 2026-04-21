using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetAllProfile
{
    public class GetAllMikrotikProfilesHandler : IRequestHandler<GetAllMikrotikProfilesQuery, List<MikrotikProfileResponse>>
    {
        private readonly IMikrotikService _mikrotikService;

        public GetAllMikrotikProfilesHandler(IMikrotikService mikrotikService)
        {
            _mikrotikService = mikrotikService;
        }

        public async Task<List<MikrotikProfileResponse>> Handle(GetAllMikrotikProfilesQuery request, CancellationToken cancellationToken)
        {
            return await _mikrotikService.GetAllProfilesAsync();
        }
    }
}
