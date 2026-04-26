using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DisableUser
{
    public record DisableMikrotikUserCommand(string Username) : IRequest<bool>;
}
