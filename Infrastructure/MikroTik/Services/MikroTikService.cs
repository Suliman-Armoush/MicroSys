using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using MediatR;
using Microsoft.AspNetCore.Http;
using tik4net.Objects;
using tik4net.Objects.Ip.Hotspot;

namespace Infrastructure.MikroTik.Services
{
  public class MikrotikService : IMikrotikService
  {
    private readonly MikrotikClient _client;
    private readonly IUserService _userService;


    public MikrotikService(MikrotikClient client , IUserService userService)
    {
      _client = client;
      _userService = userService;
    }

    public Task<bool> TestConnection()
    {
      try
      {
        using var connection = _client.Connect();
        return Task.FromResult(true);
      }
      catch
      {
        return Task.FromResult(false);
      }
    }

    public async Task<List<MikrotikUserResponse>> GetUsersByDepartmentAsync()
    {
      var manager = await _userService.GetByIdAsync(_userService.UserId);
      var departmentName = manager.Department.Name;
      var result = new List<MikrotikUserResponse>();

      using var connection = _client.Connect();

      var users = connection.LoadAll<HotspotUser>();
      users = users.Where(x => x.Comment.StartsWith(manager.Department.Name));

      foreach (var user in users)
      {
        var cleanComment = user.Comment.Substring(departmentName.Length).TrimStart('-', ' ');
        result.Add(new MikrotikUserResponse
        {
          Comment = cleanComment,
          Username = user.Name,
          BytesInRaw = user.BytesIn,
          BytesOutRaw = user.BytesOut,
          Profile = user.Profile,
          LimitTotalRaw = user.LimitBytesTotal
        });
      }

      return result;
    }

    public async Task<List<MikrotikUserResponse>> GetAllUsersAsync()
    {
      var result = new List<MikrotikUserResponse>();

      using var connection = _client.Connect();

      var users = connection.LoadAll<HotspotUser>();

      foreach (var user in users)
      {
        result.Add(new MikrotikUserResponse
        {
          Comment = user.Comment,
          Username = user.Name,
          BytesInRaw = user.BytesIn,
          BytesOutRaw = user.BytesOut,
          Profile = user.Profile,
          LimitTotalRaw = user.LimitBytesTotal
        });
      }

      return result;
    }

    public async Task<List<MikrotikProfileResponse>> GetAllProfilesAsync(int maxSpeed)
    {
      var result = new List<MikrotikProfileResponse>();

      using var connection = _client.Connect();

      var profiles = connection.LoadAll<HotspotUserProfile>();

      foreach (var profile in profiles)
      {
        if (!profile.Name.StartsWith("app-"))
          continue;

        var speedPart = profile.Name.Replace("app-", "");

        if (!int.TryParse(speedPart, out int speed))
          continue;

        if (speed > maxSpeed)
          continue;

        result.Add(new MikrotikProfileResponse
        {
          Name = profile.Name,
          SharedUsers = profile.SharedUsers,
          RateLimit = profile.RateLimit
        });
      }

      return result;
    }


    public async Task<DetailedDepartmentConsumptionResponse> GetUsageByDepartmentNameAsync(string departmentName)
    {
      var allUsers = await GetAllUsersAsync();
      var searchKey = departmentName.Trim().ToLower();

      var deptUsers = allUsers.Where(u =>
      {
        var comment = (u.Comment ?? "").Trim().ToLower();
        return comment.StartsWith($"@{searchKey}") ||
                     comment.StartsWith($"#{searchKey}") ||
                     comment.StartsWith(searchKey);
      }).ToList();

      return new DetailedDepartmentConsumptionResponse
      {
        DepartmentName = departmentName,
        TotalConsumptionGB = Math.Round(deptUsers.Sum(x => x.BytesInRaw + x.BytesOutRaw) / Math.Pow(1024, 3), 2),
        Users = deptUsers.Select(u => new UserConsumptionDetail
        {
          // نستخدم الدالة المساعدة هنا لتنظيف الاسم
          UserName = CleanUserName(u.Comment ?? u.Username, departmentName),
          UsageGB = Math.Round((u.BytesInRaw + u.BytesOutRaw) / Math.Pow(1024, 3), 2)
        }).OrderByDescending(u => u.UsageGB).ToList(),
        Type = "DepartmentUsage"
      };
    }

    // دالة مساعدة لتنظيف اسم المستخدم من اسم القسم والرموز
    private string CleanUserName(string rawName, string deptName)
    {
      if (string.IsNullOrEmpty(rawName)) return "Unknown";

      var cleaned = rawName.TrimStart('@', '#', ' ').Trim();

      if (cleaned.ToLower().StartsWith(deptName.ToLower()))
      {
        cleaned = cleaned.Substring(deptName.Length).Trim();

        cleaned = cleaned.TrimStart('-', '_', ':', '.', ' ');
      }

      return string.IsNullOrWhiteSpace(cleaned) ? rawName : cleaned;
    }

    public async Task<MikrotikUserInformationResponse> CreateUserAsync(CreateMikrotikUserRequest request)
    {
      using var connection = _client.Connect();

      var cmd = connection.CreateCommand("/ip/hotspot/user/add");

      cmd.AddParameter("name", request.Username);
      cmd.AddParameter("password", request.Password);
      cmd.AddParameter("profile", request.Profile);
      cmd.AddParameter("server", request.Server);
      cmd.AddParameter("comment", request.Comment);

      if (!string.IsNullOrEmpty(request.LimitBytes))
      {
        cmd.AddParameter("limit-bytes-total", request.LimitBytes);
      }

      cmd.ExecuteNonQuery();

      return new MikrotikUserInformationResponse
      {
        Username = request.Username,
        Profile = request.Profile,
        Server = request.Server,
        Comment = request.Comment,
        LimitGB = string.IsNullOrEmpty(request.LimitBytes)
                    ? 0
                    : double.Parse(request.LimitBytes.Replace("G", ""))
      };
    }
    public async Task<MikrotikUserInformationResponse> UpdateUserAsync(UpdateMikrotikUserRequest request, string currentUsername)
    {
      using var connection = _client.Connect();

      var cmd = connection.CreateCommand("/ip/hotspot/user/set");

      cmd.AddParameter(".id", currentUsername);

      if (!string.IsNullOrEmpty(request.NewUsername)) cmd.AddParameter("name", request.NewUsername);
      if (!string.IsNullOrEmpty(request.Password)) cmd.AddParameter("password", request.Password);
      if (!string.IsNullOrEmpty(request.Profile)) cmd.AddParameter("profile", request.Profile);
      if (!string.IsNullOrEmpty(request.Server)) cmd.AddParameter("server", request.Server);
      if (!string.IsNullOrEmpty(request.Comment)) cmd.AddParameter("comment", request.Comment);

      if (!string.IsNullOrEmpty(request.LimitBytes))
      {
        cmd.AddParameter("limit-bytes-total", request.LimitBytes);
      }

      cmd.ExecuteNonQuery();

      return new MikrotikUserInformationResponse
      {
        Username = request.NewUsername ?? currentUsername,
        Comment = request.Comment,
        LimitGB = string.IsNullOrEmpty(request.LimitBytes) ? 0 : double.Parse(request.LimitBytes.Replace("G", ""))
      };
    }
    public async Task<bool> IsUserExistsAsync(string username)
    {
      using var connection = _client.Connect();
      return connection.LoadAll<HotspotUser>()
                       .Any(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<MikrotikUserInformationResponse> GetUserByNameAsync(string username)
    {
      using var connection = _client.Connect();

      var user = connection.LoadAll<HotspotUser>().FirstOrDefault(u => u.Name == username);

      if (user == null) return null;

      double? limitGb = null;
      if (user.LimitBytesTotal > 0)
      {

        limitGb = Math.Round(user.LimitBytesTotal / 1_000_000_000.0, 0);
      }

      return new MikrotikUserInformationResponse
      {
        Username = user.Name,
        Profile = user.Profile,
        Server = user.Server,
        Comment = user.Comment,
        IsDisabled = user.Disabled,
        LimitGB = limitGb
      };
    }


    public async Task<List<MikrotikUserResponse>> SearchUsersAsync(string term)
    {
      using var connection = _client.Connect();

      var allUsers = connection.LoadAll<HotspotUser>();

      if (string.IsNullOrWhiteSpace(term))
        return MapToResponseList(allUsers.Take(20).ToList());

      var filteredUsers = allUsers.Where(u =>
          (u.Name != null && u.Name.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
          (u.Comment != null && u.Comment.Contains(term, StringComparison.OrdinalIgnoreCase))
      ).ToList();

      return MapToResponseList(filteredUsers);
    }

    private List<MikrotikUserResponse> MapToResponseList(List<HotspotUser> users)
    {
      return users.Select(u => new MikrotikUserResponse
      {
        Comment = u.Comment,
        Username = u.Name,
        BytesInRaw = u.BytesIn,
        BytesOutRaw = u.BytesOut,
        Profile = u.Profile,
        LimitTotalRaw = u.LimitBytesTotal
      }).ToList();
    }

    public async Task<bool> DeleteUserAsync(string username)
    {
      using var connection = _client.Connect();

      var user = connection.LoadAll<HotspotUser>().FirstOrDefault(u => u.Name == username);

      if (user != null)
      {
        connection.Delete(user);
        return true;
      }

      return false;
    }

        public async Task<bool> UpdateUserStatusAsync(string username, bool isDisabled)
        {
            using var connection = _client.Connect();

            try
            {
                // 1. البحث عن المستخدم بواسطة الاسم
                var findCmd = connection.CreateCommand("/ip/hotspot/user/print");
                findCmd.AddParameter("?name", username);
                var users = findCmd.ExecuteList();

                if (users == null || !users.Any())
                    return false; // المستخدم غير موجود

                var user = users.First();
                if (!user.Words.TryGetValue(".id", out string userId) || string.IsNullOrEmpty(userId))
                    return false; // لا يمكن الحصول على المعرف

                // 2. تحديث حالة التعطيل
                var setStatusCmd = connection.CreateCommand("/ip/hotspot/user/set");
                setStatusCmd.AddParameter(".id", userId);
                setStatusCmd.AddParameter("disabled", isDisabled ? "yes" : "no");
                setStatusCmd.ExecuteNonQuery(); // استخدم ExecuteNonQuery بدلاً من ExecuteScalar

                // 3. إذا كان تعطيل، فقم بإزالة الجلسات النشطة والأجهزة المرتبطة
                if (isDisabled)
                {
                    // إزالة الجلسات النشطة (active)
                    var getActiveCmd = connection.CreateCommand("/ip/hotspot/active/print");
                    getActiveCmd.AddParameter("?user", username);
                    var activeSessions = getActiveCmd.ExecuteList();

                    foreach (var session in activeSessions)
                    {
                        try
                        {
                            if (session.Words.TryGetValue(".id", out string activeId))
                            {
                                var removeActiveCmd = connection.CreateCommand("/ip/hotspot/active/remove");
                                removeActiveCmd.AddParameter(".id", activeId);
                                removeActiveCmd.ExecuteNonQuery();
                            }
                        }
                        catch { /* تجاهل */ }
                    }

                    // إزالة المضيفين (hosts)
                    var getHostCmd = connection.CreateCommand("/ip/hotspot/host/print");
                    getHostCmd.AddParameter("?user", username);
                    var hosts = getHostCmd.ExecuteList();

                    foreach (var host in hosts)
                    {
                        try
                        {
                            if (host.Words.TryGetValue(".id", out string hostId))
                            {
                                var removeHostCmd = connection.CreateCommand("/ip/hotspot/host/remove");
                                removeHostCmd.AddParameter(".id", hostId);
                                removeHostCmd.ExecuteNonQuery();
                            }
                        }
                        catch { }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ للتصحيح (يمكنك استخدام ILogger)
                Console.WriteLine($"Error in UpdateUserStatusAsync: {ex.Message}");
                return false;
            }
        }


        public async Task<List<MikrotikServerResponse>> GetAllServersAsync()
    {
      using var connection = _client.Connect();

      var command = connection.CreateCommand("/ip/hotspot/print");
      var result = command.ExecuteList();

      return result.Select(sentence =>
      {
        sentence.Words.TryGetValue("name", out string name);
        sentence.Words.TryGetValue("interface", out string @interface);
        sentence.Words.TryGetValue("disabled", out string disabled);

        return new MikrotikServerResponse
        {
          Name = name ?? "unknown",
          Interface = @interface ?? "unknown",
          IsEnabled = disabled != "true"
        };
      }).ToList();
    }

    public async Task<bool> DisableUserAsync(string username)
    {
      return await UpdateUserStatusAsync(username, true);
    }

    public async Task<bool> EnableUserAsync(string username)
    {
      return await UpdateUserStatusAsync(username, false);
    }

    public async Task<List<MikrotikHostResponse>> GetAllHostsAsync()
    {
      using var connection = _client.Connect();

      var command = connection.CreateCommand("/ip/hotspot/host/print");
      var result = command.ExecuteList();

      return result.Select(sentence =>
      {
        sentence.Words.TryGetValue("mac-address", out string mac);
        sentence.Words.TryGetValue("address", out string ip);
        sentence.Words.TryGetValue("uptime", out string uptime);
        sentence.Words.TryGetValue("comment", out string comment);

        return new MikrotikHostResponse
        {
          Comment = comment ?? "",

          MacAddress = mac ?? "unknown",
          IpAddress = ip ?? "0.0.0.0",
          Uptime = uptime ?? "00:00:00"
        };
      }).ToList();
    }

    public async Task<List<MikrotikHostResponse>> SearchHostsAsync(string term)
    {
      var allHosts = await GetAllHostsAsync();

      if (string.IsNullOrWhiteSpace(term))
        return allHosts;

      var searchKey = term.Trim().ToLower();

      return allHosts.Where(h =>
          (h.MacAddress != null && h.MacAddress.ToLower().Contains(searchKey)) ||
          (h.IpAddress != null && h.IpAddress.ToLower().Contains(searchKey)) ||
          (h.Comment != null && h.Comment.ToLower().Contains(searchKey))
      ).ToList();
    }



    public async Task<bool> RemoveHostByMacAsync(string macAddress)
    {
      try
      {
        using var connection = _client.Connect();

        var findCommand = connection.CreateCommand("/ip/hotspot/host/print");
        findCommand.AddParameter("mac-address", macAddress);

        var host = findCommand.ExecuteList().FirstOrDefault();

        if (host == null) return false;

        host.Words.TryGetValue(".id", out string internalId);
        if (string.IsNullOrEmpty(internalId)) return false;

        var deleteCommand = connection.CreateCommand("/ip/hotspot/host/remove");
        deleteCommand.AddParameter(".id", internalId);
        deleteCommand.ExecuteNonQuery();

        return true;
      }
      catch (Exception ex) when (ex.Message.Contains("!empty") || ex.Message.Contains("not supported"))
      {

        return false;
      }
    }

    public async Task<bool> RemoveAllHostsAsync()
    {
      try
      {
        using var connection = _client.Connect();

        var findCommand = connection.CreateCommand("/ip/hotspot/host/print");
        var allHosts = findCommand.ExecuteList();

        if (allHosts == null || !allHosts.Any())
          return true;


        var allIds = allHosts
            .Select(h => h.Words.TryGetValue(".id", out string id) ? id : null)
            .Where(id => !string.IsNullOrEmpty(id))
            .ToList();

        if (!allIds.Any()) return true;


        var deleteCommand = connection.CreateCommand("/ip/hotspot/host/remove");
        deleteCommand.AddParameter("numbers", string.Join(",", allIds));
        deleteCommand.ExecuteNonQuery();

        return true;
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("!empty") || ex.Message.Contains("not supported"))
          return true;

        return false;
      }
    }

    public async Task<bool> ResetAllUserCountersAsync()
    {
      try
      {
        using var connection = _client.Connect();

        var users = connection.LoadAll<HotspotUser>();

        if (users == null || !users.Any())
          return true;

        var ids = users
            .Where(u => !string.IsNullOrEmpty(u.Id))
            .Select(u => u.Id)
            .ToList();

        if (!ids.Any())
          return false;

        var command = connection.CreateCommand("/ip/hotspot/user/reset-counters");
        command.AddParameter("numbers", string.Join(",", ids));
        command.ExecuteNonQuery();

        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

  }
}
