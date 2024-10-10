using SabanaBus.Context;
using SabanaBus.Modelo;
using Microsoft.EntityFrameworkCore;
using SabanaBus.Services;

namespace SabanaBus.Repositories

{

    public class NotificationRepositories : INotificationService
    {
        private readonly SabanaBusDbContext _dbContext;

        public NotificationRepositories(SabanaBusDbContext dbContext)
        {
            _dbContext = dbContext;

        }



        public async Task<bool> CreateNotificationAsync(Notification Notification)
        {
            var dataNotification = await _dbContext.Notification.AddAsync(Notification);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteNotificationAsync(int id)
        {

            var Notification = await _dbContext.Notification.FindAsync(id);


            if (Notification == null)
            {

                throw new KeyNotFoundException($"Notification with ID {id} not found.");
            }

            _dbContext.Notification.Remove(Notification);


            await _dbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Notification>> GetAllNotificationAsync()
        {
            return await _dbContext.Notification
            .Where(s => s.IsDeleted)
            .ToListAsync();
        }



        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            var Notification = await _dbContext.Notification
               .FirstOrDefaultAsync(s => s.IDNotification == id && s.IsDeleted);

            if (Notification == null)
            {
                throw new Exception("Notification no encontrado.");
            }

            return Notification;

        }

 

        public async Task<bool> UpdateNotificationAsync(int id, Notification Notification)
        {
            try
            {
                var entity = await this._dbContext.Notification.FindAsync(id);
                if (entity != null)
                {
                    entity.FkIdSchedule = Notification.FkIdSchedule;
                    entity.Message = Notification.Message;
                    entity.DateTime = Notification.DateTime;
                    entity.Schedule = Notification.Schedule;
                    entity.IsDeleted = Notification.IsDeleted;


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