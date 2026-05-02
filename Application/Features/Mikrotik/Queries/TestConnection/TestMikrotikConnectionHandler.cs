using Application.Features.Mikrotik.Queries.TestConnection;
using Application.Interfaces;
using MediatR;

public class TestMikrotikConnectionHandler : IRequestHandler<TestMikrotikConnectionQuery, bool>
{
  private readonly IMikrotikService _service;

  public TestMikrotikConnectionHandler(IMikrotikService service)
  {
    _service = service;
  }

  public async Task<bool> Handle(TestMikrotikConnectionQuery request, CancellationToken cancellationToken)
  {
    return await _service.TestConnection();
  }
}