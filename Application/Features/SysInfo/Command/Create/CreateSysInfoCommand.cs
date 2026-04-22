using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs.Request;
using MediatR;

namespace Application.Features.SysInfo.Command.Create
{
   

    public record CreateSysInfoCommand(SysInfoRequestDto Dto) : IRequest<int>;
}
