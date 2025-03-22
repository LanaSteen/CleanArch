using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IGuestRepository
    {
        Task<List<GuestEntity>> GetAllAsync();
        Task<GuestEntity> GetByIdAsync(string id);  // Change int → string
        Task<GuestEntity> AddAsync(GuestEntity entity);
        Task<GuestEntity> UpdateAsync(GuestEntity entity);
        Task<bool> DeleteAsync(GuestEntity entity);
    }
}
