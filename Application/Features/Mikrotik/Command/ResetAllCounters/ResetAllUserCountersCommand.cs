using MediatR;

namespace Application.Features.Mikrotik.Command.ResetAllCounters
{
  public record ResetAllUserCountersCommand() : IRequest<bool>;
}