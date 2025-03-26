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
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _dbContext;

        public ReservationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReservationEntity>> GetAllAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<ReservationEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ReservationEntity> AddAsync(ReservationEntity entity)
        {
            _dbContext.Reservations.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ReservationEntity> UpdateAsync(ReservationEntity entity)
        {
            _dbContext.Reservations.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(ReservationEntity entity)
        {
            _dbContext.Reservations.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> HasReservationsForRoomAsync(int roomId)
        {
            return await _dbContext.Reservations.AnyAsync(r => r.RoomId == roomId);
        }
    }
}
