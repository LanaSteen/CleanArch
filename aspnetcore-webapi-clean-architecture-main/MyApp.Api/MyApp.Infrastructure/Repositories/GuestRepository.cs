using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Infrastructure.Data;
using MyApp.Application.DTOs.Guest;

namespace MyApp.Infrastructure.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public GuestRepository(AppDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<GuestEntity>> GetAllAsync()
        {
            return await _dbContext.Guests.ToListAsync();
        }

        public async Task<GuestEntity> GetByIdAsync(string id) 
        {
            return await _dbContext.Guests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GuestEntity> AddAsync(GuestEntity entity)
        {
            entity.PasswordHash = _passwordHasher.HashPassword(entity.PasswordHash ?? "");
            entity.Role = "Guest";  

            await _dbContext.Users.AddAsync(entity); 
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<GuestEntity> UpdateAsync(string guestId, Dictionary<string, object> updates)
        {
            var guestEntity = await _dbContext.Guests.FirstOrDefaultAsync(g => g.Id == guestId);
            if (guestEntity == null) return null;

            foreach (var update in updates)
            {
                var property = typeof(GuestEntity).GetProperty(update.Key);
                if (property != null && update.Value != null)
                {
                    property.SetValue(guestEntity, Convert.ChangeType(update.Value, property.PropertyType));
                }
            }

            await _dbContext.SaveChangesAsync();
            return guestEntity;
        }
        public async Task<bool> DeleteAsync(GuestEntity entity)
        {
            _dbContext.Users.Remove(entity);  
            _dbContext.Guests.Remove(entity); 
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
