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
        public Route Route { get; set; }
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        

        public bool IsDeleted { get; set; } 
    }
}
