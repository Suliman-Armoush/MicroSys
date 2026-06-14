using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IMikrotikService
    {
        Task<bool> TestConnectionAsync();
        Task<List<MikrotikUserResponseDto>> GetAllUsersAsync();
        Task<List<MikrotikProfileResponseDto>> GetAllProfilesAsync();
        Task<List<MikrotikServerResponseDto>> GetServersListAsync();
        Task<MikrotikUserInformationResponseDto?> CreateUserAsync(CreateMikrotikUserRequestDto user);
        Task<MikrotikUserInformationResponseDto?> GetUserByNameAsync(string username);
        Task<MikrotikUserInformationResponseDto?> UpdateUserAsync(string currentUsername, UpdateMikrotikUserRequestDto model);
        Task<bool> DeleteUserAsync(string username);
        Task<string?> DisableUserAsync(string username);
        Task<string?> EnableUserAsync(string username);
        Task<List<MikrotikUserResponseDto>> SearchUsersAsync(string term);
        Task<List<MikrotikHostResponse>> GetAllHostsAsync();
        Task<List<MikrotikHostResponse>> SearchHostsAsync(string term);
        Task<bool> RemoveHostAsync(string macAddress);
        Task<bool> RemoveAllHostsAsync();
        Task<bool> ResetAllCountersAsync();
        Task<List<MikrotikUserResponseDto>> GetUsersByDepartmentAsync();
    }
}