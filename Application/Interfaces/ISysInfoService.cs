using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISysInfoService
    {
        Task<int> CreateAsync(SysInfoRequestDto dto);
        Task<bool> UpdateAsync(SysInfo dto);
        Task<bool> DeleteAsync(int id);
        Task<SysInfo?> GetByIdAsync(int id);
        Task<IEnumerable<SysInfoResponseDto>> GetAllAsync();
    }
}
