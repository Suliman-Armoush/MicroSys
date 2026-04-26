using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.EnableUser
{
    public record EnableMikrotikUserCommand(string Username) : IRequest<bool>;
}
