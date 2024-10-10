using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;


namespace SabanaBus.Repositories
{


    public class UserTypeRepositories : IUserTypeService
    {
        private readonly SabanaBusDbContext _dbContext;

        public UserTypeRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateUserTypeAsync(UserType userType)
        {
            await _dbContext.UserType.AddAsync(userType);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task DeleteUserTypeAsync(int id)
        {
            var userType = await _dbContext.UserType.FindAsync(id);
            if (userType == null)
            {
                throw new KeyNotFoundException($"UserType with ID {id} not found.");
            }

            _dbContext.UserType.Remove(userType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserType>> GetAllUserTypesAsync()
        {
            return await _dbContext.UserType
                                   .Where(s => s.IsDeleted)
                                   .ToListAsync();
        }


        public async Task<UserType> GetUserTypeByIdAsync(int id)
        {
            var userType = await _dbContext.UserType
                                           .FirstOrDefaultAsync(s => s.IdUserType == id && s.IsDeleted);

            if (userType == null)
            {
                throw new Exception("UserType no encontrado.");
            }

            return userType;
        }

        public async Task<bool> UpdateUserTypeAsync(int id, UserType userType)
        {
            try
            {
                var entity = await this._dbContext.UserType.FindAsync(id);
                if (entity != null)
                {
                    entity.UserTypeName = userType.UserTypeName;
                    entity.User = userType.User;
                    entity.PermissionsXUserType = userType.PermissionsXUserType;
                    entity.IsDeleted = userType.IsDeleted;


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