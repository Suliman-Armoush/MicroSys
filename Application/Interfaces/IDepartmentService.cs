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
        Task AddAsync(Department department);
        Task<bool> SaveChangesAsync();
    }
}
