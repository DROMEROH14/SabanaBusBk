using SabanaBus.Context;
using SabanaBus.Modelo;
using SabanaBus.Repositories;

namespace SabanaBus.Services
{
    public interface IUserTypeService
    {
        Task<UserType> GetUserTypeByIdAsync(int id);
        Task<IEnumerable<UserType>> GetAllUserTypesAsync();
        Task<bool> CreateUserTypeAsync(UserType userType);
        Task<bool> UpdateUserTypeAsync(int id, UserType userType);
        Task DeleteUserTypeAsync(int id);
    }

}