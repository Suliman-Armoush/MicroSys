using Infrastructure.Persistence.Data;
using tik4net;
using System.Net.Sockets;

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
      throw new Exception("MikroTik settings were not found in the database.");

    if (string.IsNullOrWhiteSpace(sysInfo.MikroTikIp))
      throw new Exception("MikroTik IP address is missing.");

    if (string.IsNullOrWhiteSpace(sysInfo.Username))
      throw new Exception("MikroTik username is missing.");

    if (string.IsNullOrWhiteSpace(sysInfo.Password))
      throw new Exception("MikroTik password is missing.");

    try
    {
      var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api);

      connection.Open(
          sysInfo.MikroTikIp,
          sysInfo.Username,
          sysInfo.Password
      );

      return connection;
    }
    catch (TikConnectionException)
    {
      throw new Exception("Failed to login to MikroTik: invalid username or password.");
    }
    catch (SocketException)
    {
      throw new Exception("Unable to connect to MikroTik: please check the IP address or network connection.");
    }
    catch (TimeoutException)
    {
      throw new Exception("Connection timeout: MikroTik device is not responding.");
    }
    catch (Exception ex)
    {
      throw new Exception($"An unexpected error occurred while connecting to MikroTik: {ex.Message}");
    }
  }
}