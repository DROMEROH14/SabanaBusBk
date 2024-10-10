namespace SabanaBus.DTOs
{
    public class BusDto
    {
        public int IdBus { get; set; }
        public string LicensePlate { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
