using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.Delete
{
    public record DeleteMikrotikUserCommand(string Username) : IRequest<bool>;
}
