using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> Generate(User user);
        Task<bool> RevokeToken(string token);
    }
}
