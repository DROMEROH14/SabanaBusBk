using SabanaBus.Context;
using SabanaBus.Modelo;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface IScheduleService
    {
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<bool> CreateScheduleAsync(Schedule Schedule);
        Task<bool> UpdateScheduleAsync(int id,Schedule Schedule);
        Task DeleteScheduleAsync(int id);
    }

    
}








