using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Bus
    {
        [Key]
        public int IdBus { get; set; }
        public required string LicensePlate { get; set; }
        public required int Capacity { get; set; }
        public required bool Status { get; set; }

        public required ICollection<Assignment> Assignment  { get; set; }
   
    
    }
}
