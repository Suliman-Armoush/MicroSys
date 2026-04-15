using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        //Task<bool> ExistsAsync(string name);
        Task AddAsync(Role role);
        Task<Role?> GetByIdAsync(int id);
        Task UpdateAsync(Role role);
        Task<bool> ExistsAsync(string name, int? excludeId = null);
        Task DeleteAsync(Role role);
        Task<bool> IsRoleUsedAsync(int roleId);
        Task<List<Role>> GetAllAsync();
    }
}
