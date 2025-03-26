using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IReservationRepository
    {
        Task<List<ReservationEntity>> GetAllAsync();
        Task<ReservationEntity> GetByIdAsync(int id);
        Task<ReservationEntity> AddAsync(ReservationEntity entity);
        Task<ReservationEntity> UpdateAsync(ReservationEntity entity);
        Task<bool> DeleteAsync(ReservationEntity entity);

        Task<bool> HasReservationsForRoomAsync(int roomId);
    }
}
