using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IGuestRepository
    {
        Task<List<GuestEntity>> GetAllAsync();
        Task<GuestEntity> GetByIdAsync(string id); 
        Task<GuestEntity> AddAsync(GuestEntity entity);
        Task<GuestEntity> UpdateAsync(string guestId, Dictionary<string, object> updates);
        Task<bool> DeleteAsync(GuestEntity entity);
        Task<bool> EmailExistsAsync(string email);
        Task<UserEntity?> GetUserByGuestIdAsync(string guestId);
        //Task<bool> EmailExistsAsync(string email);
    }
}
