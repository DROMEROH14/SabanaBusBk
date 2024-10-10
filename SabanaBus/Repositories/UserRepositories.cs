using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repository
{


    public class UserRepositories : IUserService
    {
        private readonly SabanaBusDbContext _dbContext;

        public UserRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0; 
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                                   .Where(s => s.IsDeleted)
                                   .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users
                                       .FirstOrDefaultAsync(s => s.IdUser == id && s.IsDeleted);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, User user) 
        { 
            try
            {
                var entity = await this._dbContext.Users.FindAsync(id);
                if (entity != null)
                {
                    entity.FKIdUserType = user.FKIdUserType;
                    entity.Name = user.Name;
                    entity.LastName = user.LastName;
                    entity.Email = user.Email;
                    entity.Password = user.Password;
                    entity.DateCreated = user.DateCreated;
                    entity.ModifiedDate = user.ModifiedDate;
                    entity.ModifiedBy = user.ModifiedBy;
                    entity.UserType = user.UserType;
                    entity.IsDeleted = true;


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
