using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MyApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Web.Controllers
{
    [ApiController]
    [Route("api/hotel/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public AdminController(AppDbContext dbContext, IPasswordHasher<UserEntity> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDto createAdminDto)
        {
            var existingAdmin = await _dbContext.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
            if (existingAdmin != null)
            {
                return BadRequest("Only one admin user is allowed.");
            }

            var adminUser = new UserEntity
            {
                Email = createAdminDto.Email,
                Role = createAdminDto.Role
            };

            adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, createAdminDto.PasswordHash);

            _dbContext.Users.Add(adminUser);
            await _dbContext.SaveChangesAsync();

            return Ok(adminUser);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromBody] UpdateAdminDto updateAdminDto)
        {
            var adminUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role == "Admin");
            if (adminUser == null)
            {
                return NotFound("Admin user not found.");
            }

            if (!string.IsNullOrEmpty(updateAdminDto.Email))
            {
                adminUser.Email = updateAdminDto.Email;
            }

            if (!string.IsNullOrEmpty(updateAdminDto.PasswordHash))
            {
                adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, updateAdminDto.PasswordHash);
            }

            _dbContext.Users.Update(adminUser);
            await _dbContext.SaveChangesAsync();

            return Ok(adminUser);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var adminUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role == "Admin");
            if (adminUser == null)
            {
                return NotFound("Admin user not found.");
            }

            _dbContext.Users.Remove(adminUser);
            await _dbContext.SaveChangesAsync();

            return Ok("Admin user deleted successfully.");
        }
    }
}