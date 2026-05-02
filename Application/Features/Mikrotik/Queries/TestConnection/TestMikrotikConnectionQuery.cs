using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Mikrotik.Queries.TestConnection
{
  public record TestMikrotikConnectionQuery() : IRequest<bool>;
}
