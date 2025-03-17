using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelEntity>> GetHotels();
        Task<HotelEntity> GetHotelByIdAsync(int id);
        Task<HotelEntity> AddHotelAsync(HotelEntity entity);
        Task<HotelEntity> UpdateHotelAsync(int hotelId, HotelEntity entity);
        Task<(bool IsSuccess, string Message)> DeleteHotelAsync(int hotelId);


        //Task<List<HotelEntity>> GetAllHotelsAsync();
        Task<List<HotelEntity>> GetHotelsByFilterAsync(string? country, string? city, int? minRating, int? maxRating);
    }
}
