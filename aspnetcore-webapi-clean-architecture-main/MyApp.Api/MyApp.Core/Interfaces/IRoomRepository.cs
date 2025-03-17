using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<RoomEntity> CreateRoomAsync(RoomEntity room);
        Task<RoomEntity> GetRoomByIdAsync(int roomId);
        Task<List<RoomEntity>> GetAllRoomsAsync();
        Task<RoomEntity> UpdateRoomAsync(RoomEntity room);
        Task<bool> DeleteRoomAsync(int roomId);
    }
}
