using Application.DTOs.Responce;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
  public interface IUserService
  {
    Task<User> GetByIdAsync(int id);
    Task<ICollection<User>> GetAllAsync();
    Task<bool> CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(User user);
    Task<bool> SaveAsync();
  }
}
