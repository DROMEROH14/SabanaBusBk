using SabanaBus.Context;
using SabanaBus.Modelo;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IPermissionsXUserTypesService
    {
        Task<PermissionsXUserTypes> GetPermissionsXUserTypesByIdAsync(int id);
        Task<IEnumerable<PermissionsXUserTypes>> GetAllPermissionsXUserTypessAsync();
        Task<bool> CreatePermissionsXUserTypesAsync(PermissionsXUserTypes PermissionsXUserTypes);
        Task<bool> UpdatePermissionsXUserTypesAsync(int id,PermissionsXUserTypes PermissionsXUserTypes);
        Task DeletePermissionsXUserTypesAsync(int id);
    }

}







