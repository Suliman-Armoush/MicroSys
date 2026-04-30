using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDto>> GetAllAsync();
        Task<DepartmentResponseDto> GetByIdAsync(int id);
        Task<DepartmentResponseDto> CreateAsync(DepartmentRequestDto dto);
        Task<DepartmentResponseDto> UpdateAsync(int id, DepartmentRequestDto dto);
        Task DeleteAsync(int id);
    }
}
