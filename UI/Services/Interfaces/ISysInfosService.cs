using UI.Models.Request;
using UI.Models.Response;

namespace UI.Services.Interfaces
{
  public interface ISysInfosService
  {
    Task<List<SysInfosResponseDto>> GetAsync();
    Task UpdateAsync(int id, SysInfosRequestDto dto);
  }
}