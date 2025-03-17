using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _dbContext;

        public GuestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GuestEntity>> GetAllAsync()
        {
            return await _dbContext.Guests.ToListAsync();
        }

        public async Task<GuestEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Guests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GuestEntity> AddAsync(GuestEntity entity)
        {
            _dbContext.Guests.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<GuestEntity> UpdateAsync(GuestEntity entity)
        {
            _dbContext.Guests.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(GuestEntity entity)
        {
            _dbContext.Guests.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
