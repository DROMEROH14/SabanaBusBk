using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Schedule
    {
        [Key]
        public int IDSchedule { get; set; }
        public int FkIDRoute { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Frequency { get; set; }
        public bool Status { get; set; }

        public required ICollection<Notification> Notifications { get; set; }
        public required Route Route { get; set; }
    }
}
