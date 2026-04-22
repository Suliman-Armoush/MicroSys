using Application.DTOs.Request;
using Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISysInfoService
    {
        Task<int> CreateAsync(SysInfoRequestDto dto);
        Task<bool> UpdateAsync(int id, SysInfoRequestDto dto);
        Task<bool> DeleteAsync(int id);
        Task<SysInfoResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<SysInfoResponseDto>> GetAllAsync();
    }
}
