using Application.DTOs.Response;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetAllUser
{
    public class GetAllMikrotikUsersHandler
    : IRequestHandler<GetAllMikrotikUsersQuery, List<MikrotikUserResponse>>
    {
        private readonly IMikrotikService _mikrotik;

        public GetAllMikrotikUsersHandler(IMikrotikService mikrotik)
        {
            _mikrotik = mikrotik;
        }

        public async Task<List<MikrotikUserResponse>> Handle(
            GetAllMikrotikUsersQuery request,
            CancellationToken cancellationToken)
        {
            return await _mikrotik.GetAllUsersAsync();
        }
    }
}
