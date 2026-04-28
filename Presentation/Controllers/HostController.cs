using Application.DTOs.Response;
using Application.Features.Mikrotik.Command.DeleteAllFromHost;
using Application.Features.Mikrotik.Command.DeleteFromHost;
using Application.Features.Mikrotik.Queries.GetAllHosts;
using Application.Features.Mikrotik.Queries.SearchInHost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Host")]
    public class HostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<MikrotikHostResponse>>> GetHosts()
        {
            var hosts = await _mediator.Send(new GetAllMikrotikHostsQuery());
            return Ok(hosts);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<List<MikrotikHostResponse>>> SearchHosts([FromQuery] string term)
        {
            var hosts = await _mediator.Send(new SearchMikrotikHostsQuery(term));
            return Ok(hosts);
        }

        [HttpDelete("Delete/{macAddress}")]
        public async Task<IActionResult> RemoveHost(string macAddress)
        {
            var result = await _mediator.Send(new RemoveMikrotikHostCommand(macAddress));

            if (!result) return NotFound("Host not found or already disconnected.");

            return Ok(new { Message = $"Host with MAC {macAddress} has been kicked successfully." });
        }

        [HttpDelete("Delete/All")]
        public async Task<IActionResult> ClearAllHosts()
        {
            var result = await _mediator.Send(new RemoveAllMikrotikHostsCommand());
            if (!result) return BadRequest("Could not clear hosts list.");

            return Ok(new { Message = "All hosts have been disconnected successfully." });
        }
    }
}
