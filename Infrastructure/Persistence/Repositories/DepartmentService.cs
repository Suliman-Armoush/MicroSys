using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }
        public async Task<bool> UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);
            return await SaveChangesAsync();
        }
        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<bool> HasUsersAsync(int departmentId)
        {
            return await _context.Users.AnyAsync(u => u.DepartmentId == departmentId);
        }
        public async Task<bool> ExistsAsync(int departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.Id == departmentId);
        }
    }
}