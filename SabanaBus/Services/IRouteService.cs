using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IRouteService
    {
        Task<Modelo.Route> GetRouteByIdAsync(int id);
        Task<IEnumerable<Modelo.Route>> GetAllRoutesAsync();
        Task<bool> CreateRouteAsync(Modelo.Route Route);
        Task<bool> UpdateRouteAsync(int id, Modelo.Route Route);
        Task DeleteRouteAsync(int id);
    }    
}
