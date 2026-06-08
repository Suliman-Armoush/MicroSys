using Application.Features.Mikrotik.Command.AddUser;
using Application.Features.Mikrotik.Queries.GetAllUser;
using Application.Features.Mikrotik.Queries.UsersByDepartment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Microtik
{
  [ApiController]
  [Authorize(Roles = "Manager")]
  [Route("api/Manager")]
  public class ManagerController : ControllerBase
  {
    private readonly IMediator _mediator;

    public ManagerController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet("UsersByDepartment")]
    public async Task<IActionResult> GetUsersByDepartment()
    {
      var result = await _mediator.Send(new GetUsersByDepartmentQuery());
      return Ok(result);
    }


    [HttpPost("Add/User")]
    public async Task<IActionResult> AddMikrotikUser([FromBody] AddMikrotikUserCommand command)
    {
      var result = await _mediator.Send(command);
      return Ok(result);
    }
  }
}
