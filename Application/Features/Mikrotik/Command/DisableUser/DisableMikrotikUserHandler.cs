using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Application.Features.Mikrotik.Command.DisableUser
{
  public class DisableMikrotikUserHandler : IRequestHandler<DisableMikrotikUserCommand, bool>
  {
    private readonly IMikrotikService _service;
    private readonly IUserService _userService;
    public DisableMikrotikUserHandler(IMikrotikService service, IUserService userService)
    {
      _service = service;
      _userService = userService;
    }

    public async Task<bool> Handle(DisableMikrotikUserCommand request, CancellationToken ct)
    {
      var user = await _userService.GetByIdAsync(_userService.UserId);
      if (!user.ChangePerm)
      { 
        throw new UnauthorizedAccessException("User does not have permission to change this setting");
      }
      return await _service.DisableUserAsync(request.Username);
    }
  }
}
