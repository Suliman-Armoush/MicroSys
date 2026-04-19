using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Data;

using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserNameUniqueAsync(string UserName, int? excludeId = null)
        {
            return !await _context.Users.AnyAsync(u => u.UserName == UserName && u.Id != excludeId);
        }

        public async Task<User?> GetByUserNameAsync(string UserName)
        {

            return await _context.Users
                 .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.UserName == UserName);
        }
    }
}
