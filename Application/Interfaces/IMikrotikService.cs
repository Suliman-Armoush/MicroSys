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
        Task<List<MikrotikUserResponse>> GetAllUsersAsync();
        Task<List<MikrotikProfileResponse>> GetAllProfilesAsync();
        Task<DetailedDepartmentConsumptionResponse> GetUsageByDepartmentNameAsync(string departmentName);

        Task<MikrotikUserInformationResponse> CreateUserAsync(CreateMikrotikUserRequest request);
        Task<MikrotikUserInformationResponse> UpdateUserAsync(UpdateMikrotikUserRequest request, string currentUsername);
        Task<bool> IsUserExistsAsync(string username);
        Task<MikrotikUserInformationResponse> GetUserByNameAsync(string username);
        Task<List<MikrotikUserInformationResponse>> SearchUsersAsync(string term);
        Task<bool> DeleteUserAsync(string username);

        Task<bool> UpdateUserStatusAsync(string username, bool isDisabled);
        Task<List<MikrotikServerResponse>> GetAllServersAsync();
    }
}