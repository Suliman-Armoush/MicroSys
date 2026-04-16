using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.User.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserRequestDto dto)
        {
            var result = await _mediator.Send(new CreateUserCommand(dto));
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
    }
}
