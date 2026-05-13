using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Mikrotik.Command.Create;
using Application.Features.Mikrotik.Command.Delete;
using Application.Features.Mikrotik.Command.DeleteAllFromHost;
using Application.Features.Mikrotik.Command.DeleteFromHost;
using Application.Features.Mikrotik.Command.DisableUser;
using Application.Features.Mikrotik.Command.EnableUser;
using Application.Features.Mikrotik.Command.Update;
using Application.Features.Mikrotik.Queries.DepartmentConsumption;
using Application.Features.Mikrotik.Queries.GetAllHosts;
using Application.Features.Mikrotik.Queries.GetAllProfile;
using Application.Features.Mikrotik.Queries.GetAllServer;
using Application.Features.Mikrotik.Queries.GetAllUser;
using Application.Features.Mikrotik.Queries.GetMyDepartmentConsumption;
using Application.Features.Mikrotik.Queries.GetUser;
using Application.Features.Mikrotik.Queries.ReportDetailedConsumption;
using Application.Features.Mikrotik.Queries.ReportTotalConsumption;
using Application.Features.Mikrotik.Queries.SearchInHost;
using Application.Features.Mikrotik.Queries.SearchUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Application.Features.Mikrotik.Command.ResetAllCounters;
using Application.Features.Mikrotik.Queries.TestConnection;
namespace Presentation.Controllers.Microtik
{
  [ApiController]
  [Route("api/Mikrotik")]
  public class MikrotikController : ControllerBase
  {
    private readonly IMediator _mediator;

    public MikrotikController(IMediator mediator)
    {
      _mediator = mediator;
    }


    [HttpGet("TestConnection")]
    public async Task<IActionResult> TestConnection()
    {
      var result = await _mediator.Send(new TestMikrotikConnectionQuery());

      if (result)
        return Ok(new { status = "Connected ✅", success = true });

      return StatusCode(503, new { status = "Failed ❌", success = false });
    }


    [HttpGet("Users")]
    public async Task<IActionResult> GetAllUsers()
    {
      var result = await _mediator.Send(new GetAllMikrotikUsersQuery());
      return Ok(result);
    }

    [HttpGet("Profiles")]
    public async Task<IActionResult> GetAllProfiles()
    {
      var result = await _mediator.Send(new GetAllMikrotikProfilesQuery());
      return Ok(result);
    }


    [HttpPost("Create")]
    public async Task<ActionResult<MikrotikUserInformationResponse>> CreateUser([FromBody] CreateMikrotikUserCommand command)
    {
      var result = await _mediator.Send(command);
      return Ok(result);
    }
    [HttpPut("update/{currentUsername}")]
    public async Task<ActionResult<MikrotikUserInformationResponse>> UpdateUser([FromRoute] string currentUsername, [FromBody] UpdateMikrotikUserCommand command)
    {
      command.CurrentUsername = currentUsername;

      var result = await _mediator.Send(command);
      return Ok(result);
    }

    [HttpGet("Get/User/{username}")]
    public async Task<ActionResult<MikrotikUserInformationResponse>> GetUser(string username)
    {
      // الهاندلر سيتكفل بالتحقق ورمي خطأ إذا لم يجد اليوزر
      var result = await _mediator.Send(new GetMikrotikUserByNameQuery(username));

      return Ok(result);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<List<MikrotikUserResponse>>> Search([FromQuery] string term)
    {
      var results = await _mediator.Send(new SearchMikrotikUsersQuery(term));

      return Ok(results);
    }


    [HttpDelete("Delete/{username}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string username)
    {
      var result = await _mediator.Send(new DeleteMikrotikUserCommand(username));

      return Ok(new { message = "Deleted successfully" });

    }


    [HttpPut("{username}/Disable")]
    public async Task<IActionResult> DisableUser(string username)
    {
      try
      {
        var result = await _mediator.Send(new DisableMikrotikUserCommand(username));
        return Ok(new { success = true, message = "User disabled successfully." });
      }
      catch (ValidationException ex)
      {
        return BadRequest(ex.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation failed");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("{username}/Enable")]
    public async Task<IActionResult> EnableUser(string username)
    {
      try
      {
        var result = await _mediator.Send(new EnableMikrotikUserCommand(username));

        return Ok(new { success = true, message = "User enabled successfully." });
      }
      catch (ValidationException ex)
      {
        return BadRequest(ex.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation failed");
      }
      catch (Exception ex)
      {
        return StatusCode(500, "Internal server error: " + ex.Message);
      }
    }

    [HttpGet("Get/Servers")]
    public async Task<ActionResult<List<MikrotikServerResponse>>> GetServers()
    {
      var servers = await _mediator.Send(new GetAllMikrotikServersQuery());
      return Ok(servers);
    }

    [HttpDelete("ResetAllCounters")]
    public async Task<IActionResult> ResetAllCounters()
    {
      try
      {
        var result = await _mediator.Send(new ResetAllUserCountersCommand());

        if (!result)
          return BadRequest(new { success = false, message = "Failed to reset all counters." });

        return Ok(new
        {
          success = true,
          message = "All user counters have been reset successfully."
        });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new
        {
          success = false,
          message = ex.Message
        });
      }
    }



  }
}
