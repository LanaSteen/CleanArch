using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> RegisterUserAsync(string email, string password, string role)
        {
            var user = new UserEntity
            {
                Email = email,
                Role = role
            };

            return await _userRepository.AddAsync(user, password);
        }

        public async Task<UserEntity> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !_userRepository.VerifyPassword(user, password))
            {
                return null;
            }

            return user;
        }
    }
}