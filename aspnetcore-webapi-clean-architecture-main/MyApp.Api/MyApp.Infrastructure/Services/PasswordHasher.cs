using Microsoft.AspNetCore.Identity;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<UserEntity> _passwordHasher = new PasswordHasher<UserEntity>();

        public string HashPassword(string password)
        {
            var user = new UserEntity(); 
            return _passwordHasher.HashPassword(user, password);
        }
    }
}
