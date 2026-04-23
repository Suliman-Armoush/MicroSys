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

            // تحويل اسم القسم للأحرف الصغيرة وإزالة الفراغات لضمان دقة المقارنة
            var searchKey = departmentName.Trim().ToLower();

            var deptUsers = allUsers.Where(u =>
            {
                // تنظيف الكومنت وتحويله للأحرف الصغيرة قبل الفحص
                var comment = (u.Comment ?? "").Trim().ToLower();

                // الفحص المرن للحالات الثلاث: @saray أو #saray أو saray مباشرة
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
                    UserName = u.Comment ?? u.Username,
                    UsageGB = Math.Round((u.BytesInRaw + u.BytesOutRaw) / Math.Pow(1024, 3), 2)
                }).OrderByDescending(u => u.UsageGB).ToList(),
                Type = "DepartmentUsage" // تعبئة الحقل لتجنب الـ null إذا كنت تحتاجه
            };
        }
    }
}
