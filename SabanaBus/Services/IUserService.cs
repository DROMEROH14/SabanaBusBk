using SabanaBus.Context;
using SabanaBus.Modelo;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(User user); 
        Task<bool> UpdateUserAsync(int id, User user); 
        Task DeleteUserAsync(int id);
    }

  
}
