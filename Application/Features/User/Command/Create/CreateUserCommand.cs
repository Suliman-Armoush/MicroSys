using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Create
{
  public record CreateUserCommand(UserRequestDto UserDto) : IRequest<UserResponseDto>;
}
