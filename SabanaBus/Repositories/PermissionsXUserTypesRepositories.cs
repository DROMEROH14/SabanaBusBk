using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repository
{


    public class PermissionsXUserTypesRepositories: IPermissionsXUserTypesService
    {
        private readonly SabanaBusDbContext _dbContext;

        public PermissionsXUserTypesRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreatePermissionsXUserTypesAsync(PermissionsXUserTypes PermissionsXUserTypes)
        {
            var dataPermissionsXUserTypes = await _dbContext.PermissionsXUserTypes.AddAsync(PermissionsXUserTypes);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task DeletePermissionsXUserTypesAsync(int id)
        {
            var PermissionsXUserTypes = await _dbContext.PermissionsXUserTypes.FindAsync(id);

            if (PermissionsXUserTypes == null)
            {
                throw new KeyNotFoundException($"PermissionsXUserTypes with ID {id} not found.");
            }

            _dbContext.PermissionsXUserTypes.Remove(PermissionsXUserTypes);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PermissionsXUserTypes>> GetAllPermissionsXUserTypessAsync()// Método pluralizado corregido
        {
            return await _dbContext.PermissionsXUserTypes
                .Where(s => s.IsDeleted)
                .ToListAsync();
        }



        public async Task<PermissionsXUserTypes> GetPermissionsXUserTypesByIdAsync(int id)
        {
            var PermissionsXUserTypes = await _dbContext.PermissionsXUserTypes
                .FirstOrDefaultAsync(s => s.IdPermissionsXUserTypes == id && s.IsDeleted);

            if (PermissionsXUserTypes == null)
            {
                throw new Exception("PermissionsXUserTypes no encontrado.");
            }

            return PermissionsXUserTypes;
        }

        public async Task<bool>UpdatePermissionsXUserTypesAsync(int id, PermissionsXUserTypes PermissionsXUserTypes)
        {
            try
            {

                var entity = await this._dbContext.PermissionsXUserTypes.FindAsync(id);
                if (entity != null)
                {
                    
                    entity.FKIdPermission = PermissionsXUserTypes.FKIdPermission;
                    entity.FKIdUserType = PermissionsXUserTypes.FKIdUserType;
                    entity.Permissions = PermissionsXUserTypes.Permissions;
                    entity.IsDeleted = PermissionsXUserTypes.IsDeleted;

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


