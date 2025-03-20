using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IManagerRepository
    {
        Task<List<ManagerEntity>> GetAllAsync();
        Task<ManagerEntity> GetByIdAsync(int id);
        Task<ManagerEntity> GetManagerByHotelIdAsync(int hotelId);
        Task<ManagerEntity> AddAsync(ManagerEntity manager, string password);
        Task<ManagerEntity> UpdateAsync(ManagerEntity manager);
        Task<bool> DeleteAsync(ManagerEntity manager);
        Task<UserEntity> GetUserByManagerIdAsync(int managerId); 
        Task UpdateUserAsync(UserEntity user); 
    }
}
