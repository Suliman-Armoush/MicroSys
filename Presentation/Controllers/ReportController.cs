using Application.Features.Mikrotik.Command.DeleteAllFromHost;
using Application.Features.Mikrotik.Queries.DepartmentConsumption;
using Application.Features.Mikrotik.Queries.GetMyDepartmentConsumption;
using Application.Features.Mikrotik.Queries.ReportDetailedConsumption;
using Application.Features.Mikrotik.Queries.ReportTotalConsumption;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Report")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Export/Departments/Consumption")]
        public async Task<IActionResult> ExportExcel()
        {
            

            var fileBytes = await _mediator.Send(new ExportMikrotikExcelQuery());

            string fileName = $"Mikrotik_Consumption_{DateTime.Now:yyyyMMdd}.xlsx";

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

        [HttpGet("Departments/Consumption")]
        public async Task<IActionResult> GetDepartmentsConsumption()
        {
            var result = await _mediator.Send(new GetDepartmentsConsumptionQuery());

            return Ok(result);
        }

        [Authorize]
        [HttpGet("MyDepartment/Usage")]
        public async Task<IActionResult> GetMyDepartmentUsage()
        {
            var deptIdClaim = User.FindFirst("DepartmentId")?.Value;

            if (string.IsNullOrEmpty(deptIdClaim))
                return Unauthorized("لم يتم العثور على معرف القسم في بيانات الدخول.");

            var result = await _mediator.Send(new GetMyDepartmentConsumptionQuery(int.Parse(deptIdClaim)));

            return Ok(result);
        }
    }
}
