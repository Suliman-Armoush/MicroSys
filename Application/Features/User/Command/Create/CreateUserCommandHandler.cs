using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Command.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.User>(request.UserDto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);

            await _userService.AddAsync(user);

            var fullUser = await _userService.GetByIdAsync(user.Id);

            return _mapper.Map<UserResponseDto>(fullUser);
        }
    }
}
