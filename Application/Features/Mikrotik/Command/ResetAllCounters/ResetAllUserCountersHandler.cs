using Application.Interfaces;
using MediatR;

namespace Application.Features.Mikrotik.Command.ResetAllCounters
{
  public class ResetAllUserCountersHandler : IRequestHandler<ResetAllUserCountersCommand, bool>
  {
    private readonly IMikrotikService _service;

    public ResetAllUserCountersHandler(IMikrotikService service)
    {
      _service = service;
    }

    public async Task<bool> Handle(ResetAllUserCountersCommand request, CancellationToken ct)
    {
      return await _service.ResetAllUserCountersAsync();
    }
  }
}