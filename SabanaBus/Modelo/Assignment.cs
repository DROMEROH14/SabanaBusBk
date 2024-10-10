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

        public  Bus Bus { get; set; }
        public  Route Route { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

}
