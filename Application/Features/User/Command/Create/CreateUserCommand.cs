using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Create
{
    public class CreateUserCommand : IRequest<UserResponseDto>
    {
        public UserRequestDto UserDto { get; set; }
        public CreateUserCommand(UserRequestDto userDto) => UserDto = userDto;
    }
}
