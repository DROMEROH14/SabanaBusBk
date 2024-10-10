using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IBusService
    {
        Task<Bus> GetBusByIdAsync(int id);
        Task<IEnumerable<Bus>> GetAllBusAsync();
        Task<bool> CreateBusAsync(Bus Bus);
        Task<bool> UpdateBusAsync(int id, Bus Bus);
        Task DeleteBusAsync(int id);
    }

}