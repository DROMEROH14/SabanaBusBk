using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Bus
    {
        [Key]
        public int IdBus { get; set; }
        public  string LicensePlate { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }

        public List<Assignment> Assignment { get; set; } = new List<Assignment>();

        public bool IsDeleted { get; set; }


    }
}
