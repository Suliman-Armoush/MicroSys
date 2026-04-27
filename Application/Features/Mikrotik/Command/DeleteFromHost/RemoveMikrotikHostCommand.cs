using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Command.DeleteFromHost
{
    public record RemoveMikrotikHostCommand(string MacAddress) : IRequest<bool>;
}
