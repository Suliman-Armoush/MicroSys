using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetAllProfile
{
    public class GetAllMikrotikProfilesQuery : IRequest<List<MikrotikProfileResponse>>
    {
    }
}
