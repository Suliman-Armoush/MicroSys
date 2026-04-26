using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.SearchUser
{
    public record SearchMikrotikUsersQuery(string Term) : IRequest<List<MikrotikUserInformationResponse>>;
}
