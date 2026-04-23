using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.ReportDetailedConsumption
{
    public class ExportDetailedMikrotikReportQuery : IRequest<byte[]>
    {
    }
}
