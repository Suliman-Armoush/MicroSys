// Application/Features/User/Queries/Login/LoginQueryHandler.cs
using Application.DTOs.Response;
using Application.Features.User.Queries.Login;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging; // أضف هذا

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDto>
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly ILogger<LoginQueryHandler> _logger;

    public LoginQueryHandler(
        IUserService userService,
        IJwtService jwtService,
        IMapper mapper,
        ILogger<LoginQueryHandler> logger)
    {
        _userService = userService;
        _jwtService = jwtService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Login attempt for user: {UserName}", request.LoginDto.UserName);

            var user = await _userService.GetByUserNameAsync(request.LoginDto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt for user: {UserName}", request.LoginDto.UserName);
                throw new UnauthorizedAccessException("Invalid UserName or Password.");
            }

            var token = await _jwtService.Generate(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Logged in successfully",
                Token = token,
                UserName = user.UserName,
                Role = user.Role?.Name
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user: {UserName}", request.LoginDto.UserName);
            throw;
        }
    }
}