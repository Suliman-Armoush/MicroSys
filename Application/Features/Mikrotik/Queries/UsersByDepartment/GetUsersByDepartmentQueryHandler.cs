using Application.DTOs.Response;
using Application.Features.Mikrotik.Queries.GetAllUser;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.UsersByDepartment
{
  internal class GetUsersByDepartmentQueryHandler

    : IRequestHandler<GetUsersByDepartmentQuery, List<MikrotikUserResponse>>
  {
    private readonly IMikrotikService _mikrotik;

    public GetUsersByDepartmentQueryHandler(IMikrotikService mikrotik)
    {
      _mikrotik = mikrotik;
    }

    public async Task<List<MikrotikUserResponse>> Handle(
        GetUsersByDepartmentQuery request,
        CancellationToken cancellationToken)
    {
      return await _mikrotik.GetUsersByDepartmentAsync();
    }
  }
}
