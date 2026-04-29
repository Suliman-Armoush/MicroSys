using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetUser
{
    public record GetMikrotikUserByNameQuery(string Username) : IRequest<MikrotikUserInformationResponse>;
}
