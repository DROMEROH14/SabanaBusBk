using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Notification
    {
        [Key]
        public int IDNotification { get; set; }
        public int FkIdSchedule { get; set; }
        public  string Message { get; set; }
        public DateTime DateTime { get; set; }

        public  Schedule Schedule { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

}
