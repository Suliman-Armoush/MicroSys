using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Data;
using tik4net;

public class MikrotikClient
{
  private readonly DataContext _context;

  public MikrotikClient(DataContext context)
  {
    _context = context;
  }

  public ITikConnection Connect()
  {
    var sysInfo = _context.SysInfos.FirstOrDefault();

    if (sysInfo == null)
      throw new Exception("SysInfo not found in database");

    if (string.IsNullOrEmpty(sysInfo.MikroTikIp))
      throw new Exception("MikroTik IP is null");

    var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api);

    connection.Open(
        sysInfo.MikroTikIp,
        sysInfo.Username,
        sysInfo.Password
    );

    return connection;
  }
}