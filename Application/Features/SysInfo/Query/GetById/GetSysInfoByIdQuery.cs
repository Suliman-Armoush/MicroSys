using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Query.GetById
{
    public record GetSysInfoByIdQuery(int Id) : IRequest<SysInfoResponseDto?>;
}
