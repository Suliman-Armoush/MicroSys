using Application.Features.Mikrotik.Queries.GetAllUser;
using Application.Features.Mikrotik.Queries.UsersByDepartment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Microtik
{
  [ApiController]
  [Route("api/Manager")]
  public class ManagerController : ControllerBase
  {
    private readonly IMediator _mediator;

    public ManagerController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [Authorize(Roles = "Manager")]
    [HttpGet("UsersByDepartment")]
    public async Task<IActionResult> GetUsersByDepartment()
    {
      var result = await _mediator.Send(new GetUsersByDepartmentQuery());
      return Ok(result);
    }
  }
}
