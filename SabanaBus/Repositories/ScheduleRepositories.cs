using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SabanaBus.Repository
{


    public class ScheduleRepositories : IScheduleService
    {
        private readonly SabanaBusDbContext _dbContext;

        public ScheduleRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateScheduleAsync(Schedule Schedule)
        {
            var dataSchedule = await _dbContext.Schedule.AddAsync(Schedule);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }

            return true;
        
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var Schedule = await _dbContext.Schedule.FindAsync(id);

            if (Schedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {id} not found.");
            }

            _dbContext.Schedule.Remove(Schedule);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()  
        {
            return await _dbContext.Schedule
                .Where(s => s.IsDeleted)
                .ToListAsync();
        }


        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            var Schedule = await _dbContext.Schedule
                .FirstOrDefaultAsync(s => s.IDSchedule == id && s.IsDeleted);

            if (Schedule == null)
            {
                throw new Exception("Schedule no encontrado.");
            }

            return Schedule;
        }

        public async Task<bool> UpdateScheduleAsync(int id, Schedule Schedule)
        {
            try
            {
                var entity = await this._dbContext.Schedule.FindAsync(id);
                if (entity != null)
                {
                    entity.FkIDRoute = Schedule.FkIDRoute;
                    entity.DepartureTime = Schedule.DepartureTime;
                    entity.Frequency = Schedule.Frequency;
                    entity.Status = Schedule.Status;
                    entity.Notifications = Schedule.Notifications;
                    entity.Route = Schedule.Route;
                    entity.IsDeleted = Schedule.IsDeleted;


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
