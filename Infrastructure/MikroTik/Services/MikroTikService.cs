using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Features.Mikrotik.Queries.GetMyDepartmentConsumption;
using Application.Interfaces;
using Infrastructure.MikroTik.Client;
using MediatR;
using tik4net.Objects;
using tik4net.Objects.Ip.Hotspot;

namespace Infrastructure.MikroTik.Services
{
    public class MikrotikService : IMikrotikService
    {
        private readonly MikrotikClient _client;

        public MikrotikService(MikrotikClient client)
        {
            _client = client;
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
                    BytesOutRaw = user.BytesOut
                });
            }

            return result;
        }

        public async Task<List<MikrotikProfileResponse>> GetAllProfilesAsync()
        {
            var result = new List<MikrotikProfileResponse>();

            using var connection = _client.Connect();

            var profiles = connection.LoadAll<HotspotUserProfile>();

            foreach (var profile in profiles)
            {
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
            var newUser = new HotspotUser
            {
                Name = request.Username,
                Password = request.Password,
                Profile = request.Profile,
                Server = request.Server,
                Comment = request.Comment,
                LimitBytesTotal = request.LimitBytes ?? 0,
                Disabled = false
            };

            connection.Save(newUser);

            return new MikrotikUserInformationResponse
            {
                Username = request.Username,
                Profile = request.Profile,
                Server = request.Server,
                Comment = request.Comment,
                LimitGB = request.LimitBytes.HasValue ? (request.LimitBytes.Value / Math.Pow(1024, 3)) : 0
            };
        }
        public async Task<MikrotikUserInformationResponse> UpdateUserAsync(UpdateMikrotikUserRequest request, string currentUsername)
        {
            using var connection = _client.Connect();

            var existingUser = connection.LoadAll<HotspotUser>()
                                         .FirstOrDefault(u => u.Name == currentUsername);

            if (existingUser == null) throw new Exception("User not found on Mikrotik.");

            if (!string.IsNullOrEmpty(request.NewUsername))
                existingUser.Name = request.NewUsername;

            if (!string.IsNullOrEmpty(request.Password))
                existingUser.Password = request.Password;

            if (!string.IsNullOrEmpty(request.Profile))
                existingUser.Profile = request.Profile;

            if (!string.IsNullOrEmpty(request.Server))
            {
                existingUser.Server = request.Server;
            }

           
            if (request.LimitBytes.HasValue && request.LimitBytes.Value > 0)
            {
                existingUser.LimitBytesTotal = request.LimitBytes.Value;
            }

            if (!string.IsNullOrEmpty(request.Comment))
            {
                existingUser.Comment = request.Comment;
            }

            connection.Save(existingUser);

            return new MikrotikUserInformationResponse
            {
                Username = existingUser.Name,
                Server = existingUser.Server, 
                Profile = existingUser.Profile,
                Comment = existingUser.Comment,
                LimitGB = existingUser.LimitBytesTotal > 0 ? (double)existingUser.LimitBytesTotal / 1073741824 : 0
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

            return new MikrotikUserInformationResponse
            {
                Username = user.Name,
                Comment = user.Comment, 
                Profile = user.Profile,
                Server = user.Server,
                IsDisabled = user.Disabled,
            };
        }


        public async Task<List<MikrotikUserInformationResponse>> SearchUsersAsync(string term)
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

        private List<MikrotikUserInformationResponse> MapToResponseList(List<HotspotUser> users)
        {
            return users.Select(u => new MikrotikUserInformationResponse
            {
                Username = u.Name,
                Profile = u.Profile,
                Server = u.Server,
                Comment = u.Comment,
                LimitGB = u.LimitBytesTotal > 0 ? (double)u.LimitBytesTotal / 1073741824 : 0
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
            var user = connection.LoadAll<HotspotUser>().FirstOrDefault(u => u.Name == username);

            if (user == null) return false;

            user.Disabled = isDisabled; 
            connection.Save(user);
            return true;
        }


        public async Task<List<MikrotikServerResponse>> GetAllServersAsync()
        {
            using var connection = _client.Connect();

            var servers = connection.LoadAll<HotspotServer>();

            return servers.Select(s => new MikrotikServerResponse
            {
                Name = s.Name,
                Interface = s.Interface,
                IsEnabled = !s.Disabled
            }).ToList();
        }
    }
}
