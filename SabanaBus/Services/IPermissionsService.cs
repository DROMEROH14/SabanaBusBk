using SabanaBus.Context;
using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IPermissionsService
    {
        Task<Permissions> GetPermissionsByIdAsync(int id);
        Task<IEnumerable<Permissions>> GetAllPermissionssAsync();
        Task<bool>CreatePermissionsAsync(Permissions Permissions);
        Task<bool>UpdatePermissionsAsync(int id, Permissions Permissions);
        Task DeletePermissionsAsync(int id);
    }

}