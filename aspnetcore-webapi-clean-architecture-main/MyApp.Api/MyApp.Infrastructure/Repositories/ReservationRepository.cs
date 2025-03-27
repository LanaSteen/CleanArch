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
            return await _dbContext.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Include(r => r.Guest)
                .ToListAsync();
        }

        public async Task<ReservationEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Include(r => r.Guest)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ReservationEntity> AddAsync(ReservationEntity entity)
        {
            // Validate room exists and is available
            var room = await _dbContext.Rooms.FindAsync(entity.RoomId);
            if (room == null || !room.IsAvailable)
                throw new InvalidOperationException("Room is not available");

            // Validate dates
            if (entity.CheckInDate >= entity.CheckOutDate)
                throw new InvalidOperationException("Check-out date must be after check-in date");

            // Validate no overlapping reservations
            var hasOverlap = await _dbContext.Reservations
                .AnyAsync(r => r.RoomId == entity.RoomId &&
                              r.CheckOutDate > entity.CheckInDate &&
                              r.CheckInDate < entity.CheckOutDate);

            if (hasOverlap)
                throw new InvalidOperationException("Room is already booked for these dates");

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
        public async Task<bool> HasOverlappingReservationAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            return await _dbContext.Reservations
                .AnyAsync(r => r.RoomId == roomId &&
                              r.CheckOutDate > checkIn &&
                              r.CheckInDate < checkOut);
        }
        public async Task<bool> HasReservationsForGuestAsync(string guestId)
        {
            return await _dbContext.Reservations
                .AnyAsync(r => r.GuestId == guestId);
        }
        public async Task<bool> HasOverlappingReservationAsync( int roomId, DateTime checkIn, DateTime checkOut,int? excludeReservationId = null)
        {
            var query = _dbContext.Reservations
                .Where(r => r.RoomId == roomId &&
                           r.CheckOutDate > checkIn &&
                           r.CheckInDate < checkOut);

            if (excludeReservationId.HasValue)
            {
                query = query.Where(r => r.Id != excludeReservationId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
