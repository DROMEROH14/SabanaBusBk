using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Notification
    {
        [Key]
        public int IDNotification { get; set; }
        public int FkIdSchedule { get; set; }
        public required string Message { get; set; }
        public DateTime DateTime { get; set; }

        public required Schedule Schedule { get; set; }
    }
}
