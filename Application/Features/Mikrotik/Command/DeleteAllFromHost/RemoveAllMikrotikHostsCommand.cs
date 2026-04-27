using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DeleteAllFromHost
{
    public record RemoveAllMikrotikHostsCommand() : IRequest<bool>;
}
