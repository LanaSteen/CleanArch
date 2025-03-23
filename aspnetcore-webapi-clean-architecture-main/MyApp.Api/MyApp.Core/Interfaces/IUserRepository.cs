using MyApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IUserRepository
    {
        // მომხმარებლების სიის მიღება
        Task<List<UserEntity>> GetAllAsync();

        // მომხმარებლის მიღება ID-ით
        Task<UserEntity> GetByIdAsync(string id);

        // მომხმარებლის მიღება ელ.ფოსტით
        Task<UserEntity> GetByEmailAsync(string email);

        // ახალი მომხმარებლის დამატება
        Task<UserEntity> AddAsync(UserEntity user, string password);

        // მომხმარებლის განახლება
        Task<UserEntity> UpdateAsync(UserEntity user);

        // მომხმარებლის წაშლა
        Task<bool> DeleteAsync(UserEntity user);

        // პაროლის შემოწმება
        bool VerifyPassword(UserEntity user, string password);

        // როლის მიხედვით მომხმარებლების მიღება
        Task<List<UserEntity>> GetUsersByRoleAsync(string role);
    }
}