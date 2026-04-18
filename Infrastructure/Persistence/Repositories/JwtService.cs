using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 
using Application.Interfaces;
using Infrastructure.Persistence.Data; 

namespace Infrastructure.Persistence.Repositories
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context; 

        public JwtService(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<string> Generate(User user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(100);

            var token = new JwtSecurityToken(
                 issuer: _config["JwtSettings:Issuer"],
                 audience: _config["JwtSettings:Audience"],
                 claims: claims,
                 expires: expiration,
                 signingCredentials: creds
 );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var userToken = new UserToken
            {
                Token = tokenString,
                UserId = user.Id,
            };

            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();

            return tokenString;
        }
    }
}