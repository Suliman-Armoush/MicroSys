using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.GetAllUser
{
    public class GetAllMikrotikUsersQuery : IRequest<List<MikrotikUserResponse>>
    {
    }
}
