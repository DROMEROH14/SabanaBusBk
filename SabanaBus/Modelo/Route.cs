using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Route
    {
        [Key]
        public int IdRoute { get; set; }
        public required string RouteName { get; set; }
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public TimeSpan EstimatedDuration { get; set; }

        public required ICollection<Schedule> Schedules { get; set; }

        public required ICollection<Assignment> Assignment { get; set; }
    }
}
