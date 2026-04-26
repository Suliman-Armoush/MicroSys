using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Mikrotik.Command.Create;
using Application.Features.Mikrotik.Command.Delete;
using Application.Features.Mikrotik.Command.DisableUser;
using Application.Features.Mikrotik.Command.EnableUser;
using Application.Features.Mikrotik.Command.Update;
using Application.Features.Mikrotik.Queries.DepartmentConsumption;
using Application.Features.Mikrotik.Queries.GetAllProfile;
using Application.Features.Mikrotik.Queries.GetAllServer;
using Application.Features.Mikrotik.Queries.GetAllUser;
using Application.Features.Mikrotik.Queries.GetMyDepartmentConsumption;
using Application.Features.Mikrotik.Queries.ReportDetailedConsumption;
using Application.Features.Mikrotik.Queries.ReportTotalConsumption;
using Application.Features.Mikrotik.Queries.SearchUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("Users")]
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
        [HttpGet("Departments/Consumption")]
        public async Task<IActionResult> GetDepartmentsConsumption()
        {
            var result = await _mediator.Send(new GetDepartmentsConsumptionQuery());

            return Ok(result);
        }
        [HttpGet("Export/Departments/Consumption")]
        public async Task<IActionResult> ExportExcel()
        {
            // 1. إرسال الكويري إلى الهاندلر الذي يعيد byte[]
            var fileBytes = await _mediator.Send(new ExportMikrotikExcelQuery());

            // 2. اسم الملف الذي سيظهر للمستخدم عند التحميل
            string fileName = $"Mikrotik_Consumption_{DateTime.Now:yyyyMMdd}.xlsx";

            // 3. تحديد نوع الملف (Excel) وإرجاعه كـ File
            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        [HttpGet("Export/Detailed/Consumption")]
        public async Task<IActionResult> ExportDetailedReport()
        {
            var fileBytes = await _mediator.Send(new ExportDetailedMikrotikReportQuery());
            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Detailed_Mikrotik_Report.xlsx"
            );
        }
        [Authorize]
        [HttpGet("My/Department/Usage")]
        public async Task<IActionResult> GetMyDepartmentUsage()
        {
            var deptIdClaim = User.FindFirst("DepartmentId")?.Value;

            if (string.IsNullOrEmpty(deptIdClaim))
                return Unauthorized("لم يتم العثور على معرف القسم في بيانات الدخول.");

            var result = await _mediator.Send(new GetMyDepartmentConsumptionQuery(int.Parse(deptIdClaim)));

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

        [HttpGet("search")]
        public async Task<ActionResult<List<MikrotikUserInformationResponse>>> Search([FromQuery] string term)
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
            var result = await _mediator.Send(new DisableMikrotikUserCommand(username));

                return Ok(new { message = "User disabled successfully." });

        }


        [HttpPut("{username}/Enable")]
        public async Task<IActionResult> EnableUser(string username)
        {
            var result = await _mediator.Send(new EnableMikrotikUserCommand(username));

                return Ok(new { message = "User enabled successfully." });

        }

        [HttpGet("Get/Servers")]
        public async Task<ActionResult<List<string>>> GetServers()
        {
            var servers = await _mediator.Send(new GetAllMikrotikServersQuery());
            return Ok(servers);
        }
    }
}
