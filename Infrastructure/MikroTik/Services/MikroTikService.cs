using Application.DTOs.Response;
using Application.Interfaces;
using Infrastructure.MikroTik.Client;
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
    }
}
