namespace SabanaBus.DTOs
{
    public class AssignmentDto
    {
        public int AssignmentID { get; set; }
        public int FkBusID { get; set; }
        public int FkRouteID { get; set; }
        public DateTime AssignmentDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
