using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SysInfoService : ISysInfoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SysInfoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ Create
        public async Task<int> CreateAsync(SysInfoRequestDto dto)
        {
            var entity = _mapper.Map<SysInfo>(dto);

            await _context.SysInfos.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        // ✅ Update
        public async Task<bool> UpdateAsync(int id, SysInfoRequestDto dto)
        {
            var entity = await _context.SysInfos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            entity.MikroTikIp = dto.MikroTikIp;
            entity.Username = dto.Username;
            entity.Password = dto.Password;

            _context.SysInfos.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SysInfos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            _context.SysInfos.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ Get By Id
        public async Task<SysInfoResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _context.SysInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return _mapper.Map<SysInfoResponseDto>(entity);
        }

        // ✅ Get All
        public async Task<IEnumerable<SysInfoResponseDto>> GetAllAsync()
        {
            var entities = await _context.SysInfos
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<SysInfoResponseDto>>(entities);
        }
    }
}