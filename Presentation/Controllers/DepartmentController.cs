using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Department.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
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
        public async Task<ActionResult<DepartmentResponseDto>> Create([FromForm] DepartmentRequestDto dto)
        {
            if (dto == null) return BadRequest();

            var result = await _mediator.Send(new CreateDepartmentCommand(dto));

            return Ok(result);
        }
    }
}