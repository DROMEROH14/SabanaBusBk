using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Route
    {
        [Key]
        public int IdRoute { get; set; }
        public  string RouteName { get; set; }
        public string Origin { get; set; }
        public  string Destination { get; set; }
        public TimeSpan EstimatedDuration { get; set; }


        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<Assignment> Assignment { get; set; } = new List<Assignment>();

        public bool IsDeleted { get; set; } 
    }
}
