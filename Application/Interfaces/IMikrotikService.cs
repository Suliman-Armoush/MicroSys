using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
  public interface IMikrotikService
  {
    Task<bool> TestConnection();
    Task<List<MikrotikUserResponse>> GetUsersByDepartmentAsync();

    Task<List<MikrotikUserResponse>> GetAllUsersAsync();
    Task<List<MikrotikProfileResponse>> GetAllProfilesAsync(int maxSpeed);
    Task<DetailedDepartmentConsumptionResponse> GetUsageByDepartmentNameAsync(string departmentName);

    Task<MikrotikUserInformationResponse> CreateUserAsync(CreateMikrotikUserRequest request);
    Task<MikrotikUserInformationResponse> UpdateUserAsync(UpdateMikrotikUserRequest request, string currentUsername);
    Task<bool> IsUserExistsAsync(string username);
    Task<MikrotikUserInformationResponse> GetUserByNameAsync(string username);
    Task<List<MikrotikUserResponse>> SearchUsersAsync(string term);
    Task<bool> DeleteUserAsync(string username);

    Task<bool> UpdateUserStatusAsync(string username, bool isDisabled);
    Task<List<MikrotikServerResponse>> GetAllServersAsync();

    Task<List<MikrotikHostResponse>> GetAllHostsAsync();
    Task<List<MikrotikHostResponse>> SearchHostsAsync(string term);
    Task<bool> RemoveHostByMacAsync(string macAddress);
    Task<bool> RemoveAllHostsAsync();
    Task<bool> DisableUserAsync(string username);
    Task<bool> EnableUserAsync(string username);
    Task<bool> ResetAllUserCountersAsync();

  }
}