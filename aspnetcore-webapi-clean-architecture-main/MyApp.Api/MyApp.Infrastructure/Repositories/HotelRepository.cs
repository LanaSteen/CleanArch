using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories
{
    public class HotelRepository(AppDbContext dbContext) : IHotelRepository
    {
        public async Task<IEnumerable<HotelEntity>> GetHotels()
        {
            return await dbContext.Hotels.ToListAsync();
        }

        public async Task<HotelEntity?> GetHotelByIdAsync(int id)
        {
            return await dbContext.Hotels
                .Include(h => h.Manager)
                .Include(h => h.Rooms)
                .Include(h => h.Reservations)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<HotelEntity> AddHotelAsync(HotelEntity entity)
        {
            dbContext.Hotels.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<HotelEntity> UpdateHotelAsync(int hotelId, HotelEntity entity)
        {
            var hotel = await dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == hotelId);

            if (hotel is not null)
            {
                hotel.Name = entity.Name ?? hotel.Name;
                hotel.Rating = entity.Rating >=0 ? entity.Rating : hotel.Rating;
                hotel.Country = entity.Country ?? hotel.Country;
                hotel.City = entity.City ?? hotel.City;
                hotel.Address = entity.Address ?? hotel.Address;

                await dbContext.SaveChangesAsync();
                return hotel;
            }

            return entity;
        }

        public async Task<(bool IsSuccess, string Message)> DeleteHotelAsync(int hotelId)
        {
            var hotel = await dbContext.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Reservations)
                .Include(h => h.Manager) 
                .FirstOrDefaultAsync(x => x.Id == hotelId);

            if (hotel is null)
            {
                return (false, "Hotel not found with the provided ID.");
            }

            if (hotel.Rooms.Any() || hotel.Reservations.Any())
            {
                return (false, "Hotel cannot be deleted because there are active rooms or reservations.");
            }

            if (hotel.Manager != null)
            {
                dbContext.Managers.Remove(hotel.Manager);  
            }

            dbContext.Hotels.Remove(hotel);  
            await dbContext.SaveChangesAsync();

            return (true, "Hotel and its associated manager deleted successfully.");
        }

        public async Task<List<HotelEntity>> GetHotelsByFilterAsync(string? country, string? city, int? minRating, int? maxRating)
        {
            var query = dbContext.Hotels.AsQueryable();

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(h => h.Country == country);
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                query = query.Where(h => h.City == city);
            }

            if (minRating.HasValue)
            {
                query = query.Where(h => h.Rating >= minRating.Value);
            }

            if (maxRating.HasValue)
            {
                query = query.Where(h => h.Rating <= maxRating.Value);
            }

            return await query.ToListAsync();
        }
    }
}