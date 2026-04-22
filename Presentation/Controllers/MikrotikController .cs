using Application.Features.Mikrotik.Queries.DepartmentConsumption;
using Application.Features.Mikrotik.Queries.GetAllProfile;
using Application.Features.Mikrotik.Queries.GetAllUser;
using Application.Features.Mikrotik.Queries.ReportTotalConsumption;
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
        [HttpGet("departments-consumption")]
        public async Task<IActionResult> GetDepartmentsConsumption()
        {
            var result = await _mediator.Send(new GetDepartmentsConsumptionQuery());

            return Ok(result);
        }
        [HttpGet("export-excel")]
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
    }
}
