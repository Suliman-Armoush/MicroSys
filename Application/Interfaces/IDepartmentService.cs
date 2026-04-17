using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs.Request;
using Application.DTOs.Response;
namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> AddAsync(Department department);
        Task<bool> SaveChangesAsync();

        Task<Department> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Department department);

        Task<bool> DeleteAsync(Department department);

        Task<List<Department>> GetAllAsync();
        Task<bool> HasUsersAsync(int departmentId);
        Task<bool> ExistsAsync(int departmentId);
    }
}
