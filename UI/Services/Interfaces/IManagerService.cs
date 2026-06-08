using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IManagerService
    {
        Task<DetailedDepartmentConsumptionResponseDto?> GetMyDepartmentUsageAsync();
        Task<List<MikrotikUserResponseDto>> GetUsersByDepartmentAsync();
        Task<MikrotikUserInformationResponseDto?> AddUserAsync(ManagerMikrotikUserRequestDto request);
    }
}
