using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUserService userService, IJwtService jwtService, IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByUserNameAsync(request.LoginDto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid UserName or Password.");
            }

            var token = await _jwtService.Generate(user);

            return new AuthResponseDto
            {
                Message = "Logged in successfully",
                Token = token

            };

        }
    }
}