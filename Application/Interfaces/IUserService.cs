using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Interfaces
{
  public interface IUserService
  {
    int UserId { get;}
    Task<User?> GetByIdAsync(int id);
    Task<bool> IsUserNameUniqueAsync(string UserName, int? excludeId = null);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);

    Task<User?> GetByUserNameAsync(string UserName);

  }
}
