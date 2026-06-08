using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.EnableUser
{
  public class EnableMikrotikUserHandler : IRequestHandler<EnableMikrotikUserCommand, bool>
  {
    private readonly IMikrotikService _mikrotikService;
    private readonly IUserService _userService;

    public EnableMikrotikUserHandler(IMikrotikService mikrotikService, IUserService userService)
    {
      _mikrotikService = mikrotikService;
      _userService = userService;
    }

    public async Task<bool> Handle(EnableMikrotikUserCommand request, CancellationToken cancellationToken)
    {
      var user = await _userService.GetByIdAsync(_userService.UserId);
      if (!user.ChangePerm)
      {
        throw new UnauthorizedAccessException("User does not have permission to change this setting");
      }

      return await _mikrotikService.EnableUserAsync(request.Username);
    }
  }
}
