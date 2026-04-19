using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Department.Command.Create;
using Application.Features.Department.Command.Delete;
using Application.Features.Department.Command.Update;
using Application.Features.Department.Queries.GetAll;
using Application.Features.Department.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    //[Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/Department")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<DepartmentResponseDto>> Create([FromBody] DepartmentRequestDto dto)
        {
            var result = await _mediator.Send(new CreateDepartmentCommand(dto));
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<DepartmentResponseDto>> Update(int id, [FromBody] DepartmentRequestDto dto)
        {
            var result = await _mediator.Send(new UpdateDepartmentCommand(id, dto));
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDepartmentCommand(id));
            return Ok(new { message = "Deleted successfully" });
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<DepartmentResponseDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetDepartmentByIdQuery(id));
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DepartmentResponseDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(result);
        }
    }
}