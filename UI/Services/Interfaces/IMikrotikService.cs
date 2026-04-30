using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IMikrotikService
    {
        Task<List<MikrotikUserResponseDto>> GetAllUsersAsync();
        Task<List<MikrotikProfileResponseDto>> GetAllProfilesAsync();
        Task<List<MikrotikServerResponseDto>> GetServersListAsync();
        Task<MikrotikUserInformationResponseDto?> CreateUserAsync(CreateMikrotikUserRequestDto user);


    }
}
