using SabanaBus.Modelo;
using SabanaBus.Repositories;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IAssignmentService
    {
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<IEnumerable<Assignment>> GetAllAssignmentAsync();
        Task<bool> CreateAssignmentAsync(Assignment Assignment);
        Task<bool> UpdateAssignmentAsync(int id, Assignment Assignment);
        Task DeleteAssignmentAsync(int id);
    }
}