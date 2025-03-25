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
            try
            {
                dbContext.Hotels.Add(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
       
                Console.WriteLine($"Error adding hotel: {ex.Message}");
                throw; 
            }
        }
        public async Task<HotelEntity> UpdateHotelAsync(int hotelId, HotelEntity entity)
        {
            var hotel = await dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == hotelId);

            if (hotel != null)
            {
                if (!string.IsNullOrEmpty(entity.Name))
                    hotel.Name = entity.Name;

                if (entity.Rating > 0)
                    hotel.Rating = entity.Rating;

                if (!string.IsNullOrEmpty(entity.Country))
                    hotel.Country = entity.Country;

                if (!string.IsNullOrEmpty(entity.City))
                    hotel.City = entity.City;

                if (!string.IsNullOrEmpty(entity.Address))
                    hotel.Address = entity.Address;

                await dbContext.SaveChangesAsync();
                return hotel;
            }

            return null;
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
                // First remove the manager reference from the hotel
                hotel.ManagerId = -1;
                await dbContext.SaveChangesAsync();

                // Then delete the manager
                dbContext.Managers.Remove(hotel.Manager);
                await dbContext.SaveChangesAsync();
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