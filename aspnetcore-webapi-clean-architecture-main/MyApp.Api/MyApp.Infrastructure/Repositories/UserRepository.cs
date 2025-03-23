using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public UserRepository(AppDbContext dbContext, IPasswordHasher<UserEntity> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> AddAsync(UserEntity user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(UserEntity user)
        {
            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool VerifyPassword(UserEntity user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }

        public async Task<List<UserEntity>> GetUsersByRoleAsync(string role)
        {
            return await _dbContext.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }
    }
}