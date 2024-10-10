using SabanaBus.Modelo;
using SabanaBus.Repository;

namespace SabanaBus.Services
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<IEnumerable<Notification>> GetAllNotificationAsync();
        Task<bool> CreateNotificationAsync(Notification Notification);
        Task<bool> UpdateNotificationAsync(int id, Notification Notification);
        Task DeleteNotificationAsync(int id);

    }

}