using MyApp.Core.Entities;



namespace MyApp.Core.Interfaces
{
    public interface IManagerRepository
    {
        Task<ManagerEntity> AddAsync(ManagerEntity manager);
        Task<ManagerEntity> UpdateAsync(ManagerEntity manager);
        Task<bool> DeleteAsync(ManagerEntity manager);
        Task<ManagerEntity> GetByIdAsync(int id);
        Task<List<ManagerEntity>> GetAllAsync();
        Task<ManagerEntity> GetManagerByHotelIdAsync(int hotelId);
    }
}
