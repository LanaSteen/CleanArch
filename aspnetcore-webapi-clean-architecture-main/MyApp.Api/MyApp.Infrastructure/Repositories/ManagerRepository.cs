using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public ManagerRepository(AppDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<ManagerEntity>> GetAllAsync()
        {
            return await _dbContext.Managers
                .Include(m => m.Hotel)
                .ToListAsync();
        }

        public async Task<ManagerEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Managers
                .Include(m => m.Hotel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ManagerEntity> GetManagerByHotelIdAsync(int hotelId)
        {
            return await _dbContext.Managers
                .FirstOrDefaultAsync(x => x.HotelId == hotelId);
        }

        public async Task<ManagerEntity> AddAsync(ManagerEntity manager, string password)
        {
            if (string.IsNullOrWhiteSpace(manager.Email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Manager email and password are required.");
            }

            if (manager.HotelId == null || manager.HotelId <= 0)
            {
                throw new ArgumentException("Invalid HotelId for Manager.");
            }

            var hotel = await _dbContext.Hotels.FindAsync(manager.HotelId);
            if (hotel == null)
            {
                throw new ArgumentException("Hotel not found.");
            }

            if (hotel.ManagerId != 0)
            {
                throw new InvalidOperationException("This hotel already has a manager.");
            }

            var user = new UserEntity
            {
                Email = manager.Email,
                PasswordHash = _passwordHasher.HashPassword(password),
                Role = "Manager"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            manager.UserId = user.Id;
            manager.PasswordHash = _passwordHasher.HashPassword(password);

            _dbContext.Managers.Add(manager);
            await _dbContext.SaveChangesAsync();

            hotel.ManagerId = manager.Id;
            await _dbContext.SaveChangesAsync();

            return manager;
        }
        public async Task<ManagerEntity> UpdateAsync(ManagerEntity manager)
        {
            _dbContext.Managers.Update(manager);
            await _dbContext.SaveChangesAsync();
            return manager;
        }

        public async Task<bool> DeleteAsync(ManagerEntity manager)
        {
            if (manager.HotelId.HasValue)
            {
                var hotel = await _dbContext.Hotels.FindAsync(manager.HotelId.Value);
                if (hotel != null)
                {
                    hotel.ManagerId = -1;
                }
            }

            _dbContext.Managers.Remove(manager);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<UserEntity> GetUserByManagerIdAsync(int managerId)
        {
            var manager = await _dbContext.Managers
                .Include(m => m.User) 
                .FirstOrDefaultAsync(m => m.Id == managerId);

            return manager?.User;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        private int GenerateUniqueUserId()
        {
            return new Random().Next(1, int.MaxValue); 
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Managers.AnyAsync(g => g.Email == email);
        }
    }
}
