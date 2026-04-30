using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDto>> GetAllAsync();
    }
}
