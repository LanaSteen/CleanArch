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
                .Include(r => r.Hotel) // Include related Hotel
                .Include(r => r.Reservations) // Include reservations
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<List<RoomEntity>> GetAllRoomsAsync()
        {
            return await dbContext.Rooms
                .Include(r => r.Hotel) // Include related Hotel
                .Include(r => r.Reservations) // Include reservations
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
            var room = await dbContext.Rooms.FindAsync(roomId);
            if (room == null)
                return false;

            dbContext.Rooms.Remove(room);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
