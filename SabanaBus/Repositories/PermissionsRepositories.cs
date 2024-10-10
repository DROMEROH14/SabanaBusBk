using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repository
{


    public class PermissionsRepositories : IPermissionsService
    {
        private readonly SabanaBusDbContext _dbContext;

        public PermissionsRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool>CreatePermissionsAsync(Permissions Permissions)
        {
            var dataPermissions = await _dbContext.Permissions.AddAsync(Permissions);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

    

        public async Task DeletePermissionsAsync(int id)
        {
            var Permissions = await _dbContext.Permissions.FindAsync(id);

            if (Permissions == null)
            {
                throw new KeyNotFoundException($"Permissions with ID {id} not found.");
            }

            _dbContext.Permissions.Remove(Permissions);
            await _dbContext.SaveChangesAsync();
        }



        public async Task<IEnumerable<Permissions>> GetAllPermissionssAsync()// Método pluralizado corregido
        {
            return await _dbContext.Permissions
                .Where(s => s.IsDeleted)
                .ToListAsync();
        }

 
        public async Task<Permissions> GetPermissionsByIdAsync(int id)
        {
            var Permissions = await _dbContext.Permissions
                .FirstOrDefaultAsync(s => s.IdPermission == id && s.IsDeleted);

            if (Permissions == null)
            {
                throw new Exception("Permissions no encontrado.");
            }

            return Permissions;
        }



        public async Task<bool>UpdatePermissionsAsync(int id, Permissions Permissions)
        {
            try
            {
                var entity = await this._dbContext.Permissions.FindAsync(id);
                if (entity != null)
                {
                    entity.Permission = Permissions.Permission;
                    entity.PermissionsXUserType = Permissions.PermissionsXUserType;
                    entity.IsDeleted = Permissions.IsDeleted;


                    this._dbContext.Update(entity);

                    return (await this._dbContext.SaveChangesAsync()) > 0;
                }
                return false;

            }
            catch (Exception)
            {

                throw;


            }

        }


    }
}