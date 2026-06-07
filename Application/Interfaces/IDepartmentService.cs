using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
namespace Application.Interfaces
{
  public interface IDepartmentService
  {
    Task<Department> AddDep(Department department);

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
