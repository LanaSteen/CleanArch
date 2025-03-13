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

        public async Task<HotelEntity> GetHotelByIdAsync(int id)
        {
            return await dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == id);
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
                hotel.Name = entity.Name;
                hotel.Rating = entity.Rating;
                hotel.Country = entity.Country;
                hotel.City = entity.City;
                hotel.Address = entity.Address;

                await dbContext.SaveChangesAsync();
                return hotel;
            }

            return entity;
        }

        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == hotelId);

            if (hotel is not null)
            {
                dbContext.Hotels.Remove(hotel);
                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}