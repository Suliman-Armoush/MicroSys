using Application.DTOs.Response;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetUser
{
    public class GetMikrotikUserByNameHandler : IRequestHandler<GetMikrotikUserByNameQuery, MikrotikUserInformationResponse>
    {
        private readonly IMikrotikService _service;

        public GetMikrotikUserByNameHandler(IMikrotikService service) => _service = service;

        public async Task<MikrotikUserInformationResponse> Handle(GetMikrotikUserByNameQuery request, CancellationToken ct)
        {
            var result = await _service.GetUserByNameAsync(request.Username);

            if (result == null)
                throw new KeyNotFoundException("Department not found.");

            return result;
        }
    }
}
