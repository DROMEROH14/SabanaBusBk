using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Assignment
    {
        [Key]
        public int AssignmentID { get; set; }
        public int FkBusID { get; set; }
        public int FkRouteID { get; set; }
        public DateTime AssignmentDate { get; set; }

        public required Bus Bus { get; set; }
        public required Route Route { get; set; }
    }

}
