using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Role.Command.Create;
using Application.Features.Role.Command.Delete;
using Application.Features.Role.Queries.GetAll;
using Application.Features.Role.Queries.GetById;
using Application.Interfaces; 
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Role")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRoleService _roleService; 

        public RoleController(IMediator mediator, IRoleService roleService)
        {
            _mediator = mediator;
            _roleService = roleService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<RoleResponseDto>> Create([FromBody] RoleRequestDto dto)
        {
            var exists = await _roleService.ExistsAsync(dto.Name);
            if (exists)
            {
                return BadRequest("This role name already exists.");
            }

            var result = await _mediator.Send(new CreateRoleCommand(dto));

            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<RoleResponseDto>> Update(int id, [FromBody] RoleRequestDto dto)
        {
            var existingRole = await _roleService.GetByIdAsync(id);
            if (existingRole == null)
            {
                return NotFound($"Role not found.");
            }

            var isNameDuplicate = await _roleService.ExistsAsync(dto.Name, id);
            if (isNameDuplicate)
            {
                return BadRequest("Role name already exists for another role.");
            }

            var result = await _mediator.Send(new UpdateRoleCommand(id, dto));

            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound(new { message = $"Role not found." });
            }

            var isUsed = await _roleService.IsRoleUsedAsync(id);
            if (isUsed)
            {
                return BadRequest(new
                {
                    message = "Cannot delete this role because it is currently assigned to one or more users."
                });
            }

            var result = await _mediator.Send(new DeleteRoleCommand(id));

            if (!result) return BadRequest(new { message = "An error occurred during deletion." });

            return Ok(new { message = "Role has been deleted successfully." });
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RoleResponseDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(id));

            if (result == null)
            {
                return NotFound(new { message = $"Role not found." });
            }

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<RoleResponseDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }
    }
}
