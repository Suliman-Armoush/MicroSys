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
    }
}
