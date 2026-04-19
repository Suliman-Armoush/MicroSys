using Application.DTOs.Request;
using Application.Features.User.Command.Logout;
using Application.Features.User.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var result = await _mediator.Send(new LoginQuery(loginDto));

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            var result = await _mediator.Send(new LogoutCommand(authHeader));

            if (result)
            {
                return Ok(new { Message = "Logged out successfully" });
            }

            return BadRequest(new { Message = "Logout failed" });
        }

    }
}
