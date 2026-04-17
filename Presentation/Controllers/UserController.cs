using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.User.Command.Create;
using Application.Features.User.Command.Delete;
using Application.Features.User.Command.Update;
using Application.Features.User.Queries.GetAll;
using Application.Features.User.Queries.GetByID;
using Application.Helper;
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

        [HttpPost("Register")]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserRequestDto dto)
        {
            var result = await _mediator.Send(new CreateUserCommand(dto));
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequestDto dto)
        {
            var result = await _mediator.Send(new UpdateUserCommand(id, dto));

            return Ok(result);
        }
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));

            return Ok(new { message = "Deleted successfully" });
        }
    }
}
