using Application.Features.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var result = await _mediator.Send(new GetUserQuery(id));
      return Ok(result);
    }
  }
}
