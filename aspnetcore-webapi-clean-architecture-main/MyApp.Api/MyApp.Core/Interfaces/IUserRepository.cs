using MyApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAllAsync();

        Task<UserEntity> GetByIdAsync(string id);

        Task<UserEntity> GetByEmailAsync(string email);

        Task<UserEntity> AddAsync(UserEntity user, string password);

        Task<UserEntity> UpdateAsync(UserEntity user);

        Task<bool> DeleteAsync(UserEntity user);

        bool VerifyPassword(UserEntity user, string password);

        Task<List<UserEntity>> GetUsersByRoleAsync(string role);
    }
}