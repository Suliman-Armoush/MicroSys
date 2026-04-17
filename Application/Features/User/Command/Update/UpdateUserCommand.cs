using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Update
{
    public record UpdateUserCommand(int Id, UpdateUserRequestDto UserDto) : IRequest<UserResponseDto>;
}
