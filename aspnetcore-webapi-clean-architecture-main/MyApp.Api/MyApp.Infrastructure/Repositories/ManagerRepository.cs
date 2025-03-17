using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _dbContext;

        public ManagerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return await _dbContext.Managers.FirstOrDefaultAsync(x => x.HotelId == hotelId);
        }

        public async Task<ManagerEntity> AddAsync(ManagerEntity entity)
        {
            var existingManager = await _dbContext.Managers
                .FirstOrDefaultAsync(m => m.HotelId == entity.HotelId);

            if (existingManager != null)
            {
                throw new InvalidOperationException("This hotel already has a manager.");
            }

            _dbContext.Managers.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ManagerEntity> UpdateAsync(ManagerEntity entity)
        {
            var manager = await _dbContext.Managers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (manager != null)
            {
                var existingManager = await _dbContext.Managers
                    .FirstOrDefaultAsync(m => m.HotelId == entity.HotelId && m.Id != entity.Id);

                if (existingManager != null)
                {
                    throw new InvalidOperationException("This hotel already has a manager.");
                }

                manager.FirstName = entity.FirstName;
                manager.LastName = entity.LastName;
                manager.PersonalNumber = entity.PersonalNumber;
                manager.Email = entity.Email;
                manager.PhoneNumber = entity.PhoneNumber;
                manager.HotelId = entity.HotelId;

                await _dbContext.SaveChangesAsync();
                return manager;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(ManagerEntity entity)
        {
            var manager = await _dbContext.Managers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (manager != null)
            {
                _dbContext.Managers.Remove(manager);
                return await _dbContext.SaveChangesAsync() > 0;
            }

            return false; 
        }
    }
}
