using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Query.GetAll
{
    public record GetAllSysInfoQuery() : IRequest<IEnumerable<SysInfoResponseDto>>;
}
