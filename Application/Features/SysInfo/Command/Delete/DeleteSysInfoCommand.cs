using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.SysInfo.Command.Delete
{
    public record DeleteSysInfoCommand(int Id) : IRequest<bool>;
}
