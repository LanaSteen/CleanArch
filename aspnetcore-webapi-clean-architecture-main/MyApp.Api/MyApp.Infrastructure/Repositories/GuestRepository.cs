using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Infrastructure.Data;

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

        public async Task<GuestEntity> GetByIdAsync(string id)  // Change ID to string (IdentityUser uses string IDs)
        {
            return await _dbContext.Guests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GuestEntity> AddAsync(GuestEntity entity)
        {
            entity.PasswordHash = _passwordHasher.HashPassword(entity.PasswordHash ?? "");
            entity.Role = "Guest";  // Role is set here since GuestEntity inherits UserEntity

            _dbContext.Guests.Add(entity);  // Directly add GuestEntity
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<GuestEntity> UpdateAsync(GuestEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.PasswordHash))
            {
                entity.PasswordHash = _passwordHasher.HashPassword(entity.PasswordHash);
            }

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
