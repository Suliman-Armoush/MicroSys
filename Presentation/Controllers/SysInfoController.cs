using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.SysInfo.Command.Create;
using Application.Features.SysInfo.Command.Delete;
using Application.Features.SysInfo.Command.Update;
using Application.Features.SysInfo.Query.GetAll;
using Application.Features.SysInfo.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SysInfoController : ControllerBase
{
    private readonly IMediator _mediator;

    public SysInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ✅ Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SysInfoRequestDto dto)
    {
        var result = await _mediator.Send(new CreateSysInfoCommand(dto));

        return Ok(new
        {
            Id = result,
            Message = "Created Successfully"
        });
    }

    // ✅ Update
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SysInfoRequestDto dto)
    {
        var result = await _mediator.Send(new UpdateSysInfoCommand(id, dto));

        if (!result)
            return NotFound("SysInfo not found");

        return Ok("Updated Successfully");
    }

    // ✅ Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteSysInfoCommand(id));

        if (!result)
            return NotFound("SysInfo not found");

        return Ok("Deleted Successfully");
    }

    // ✅ Get By Id
    [HttpGet("{id}")]
    public async Task<ActionResult<SysInfoResponseDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetSysInfoByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // ✅ Get All
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SysInfoResponseDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllSysInfoQuery());

        return Ok(result);
    }
}