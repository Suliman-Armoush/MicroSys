using Application.DTOs.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Command.Update
{
    public record UpdateSysInfoCommand(int Id, SysInfoRequestDto Dto) : IRequest<bool>;
}
