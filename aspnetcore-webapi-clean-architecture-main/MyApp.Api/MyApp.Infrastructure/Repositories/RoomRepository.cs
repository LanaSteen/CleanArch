using Microsoft.EntityFrameworkCore;
using MyApp.Application.Exceptions;
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
    public class RoomRepository(AppDbContext dbContext) : IRoomRepository
    {
        public async Task<RoomEntity> CreateRoomAsync(RoomEntity room)
        {
            await dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room;
        }

        public async Task<RoomEntity> GetRoomByIdAsync(int roomId)
        {
            return await dbContext.Rooms
                .Include(r => r.Hotel) 
                .Include(r => r.Reservations) 
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<List<RoomEntity>> GetAllRoomsAsync()
        {
            return await dbContext.Rooms
                .Include(r => r.Hotel) 
                .Include(r => r.Reservations) 
                .ToListAsync();
        }

        public async Task<RoomEntity> UpdateRoomAsync(RoomEntity room)
        {
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
            return room;
        }
        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            var room = await dbContext.Rooms
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
                throw new NotFoundException("Room not found");

            if (room.Reservations.Any())
                throw new InvalidOperationException("Cannot delete room because it has existing reservations");

            dbContext.Rooms.Remove(room);
            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ExistsAsync(int roomId)
        {
            return await dbContext.Rooms.AnyAsync(r => r.Id == roomId);
        }
    }
}
