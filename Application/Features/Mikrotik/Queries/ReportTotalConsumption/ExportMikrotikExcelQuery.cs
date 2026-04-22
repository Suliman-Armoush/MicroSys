using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.ReportTotalConsumption
{
    public class ExportMikrotikExcelQuery : IRequest<byte[]>
    {
    }
}
