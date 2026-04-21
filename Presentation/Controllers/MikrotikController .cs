using Application.Features.Mikrotik.Queries.GetAllProfile;
using Application.Features.Mikrotik.Queries.GetAllUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/mikrotik")]
    public class MikrotikController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MikrotikController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllMikrotikUsersQuery());
            return Ok(result);
        }

        [HttpGet("profiles")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var result = await _mediator.Send(new GetAllMikrotikProfilesQuery());
            return Ok(result);
        }
    }
}
