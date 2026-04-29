using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponseDto>> GetAllAsync();
        Task<RoleResponseDto> GetByIdAsync(int id);
        Task<RoleResponseDto> CreateAsync(RoleRequestDto dto);
        Task<RoleResponseDto> UpdateAsync(int id, RoleRequestDto dto);
        Task DeleteAsync(int id);
    }
}
