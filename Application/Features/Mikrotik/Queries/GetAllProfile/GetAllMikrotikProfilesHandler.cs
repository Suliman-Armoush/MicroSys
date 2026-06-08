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
    private readonly IUserService _userService;

    public GetAllMikrotikProfilesHandler(IMikrotikService mikrotikService, IUserService userService)
    {
      _mikrotikService = mikrotikService;
      _userService = userService;
    }

    public async Task<List<MikrotikProfileResponse>> Handle(GetAllMikrotikProfilesQuery request, CancellationToken cancellationToken)
    {
      var user = await _userService.GetByIdAsync(_userService.UserId);
      return await _mikrotikService.GetAllProfilesAsync(user.MaxSpeed);
    }
  }
}
