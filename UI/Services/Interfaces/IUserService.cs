using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
  public interface IUserService
  {
    Task<List<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto> GetByIdAsync(int id);
    Task<UserResponseDto> CreateAsync(UserRequestDto dto);
    Task<UserResponseDto> UpdateAsync(int id, UserRequestDto dto);
    Task DeleteAsync(int id);
  }
}
