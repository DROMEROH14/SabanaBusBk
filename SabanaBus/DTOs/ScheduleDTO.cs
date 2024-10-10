namespace SabanaBus.DTOs
{
    public class ScheduleDTO
    {
        public int IDSchedule { get; set; }
        public int FkIDRoute { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Frequency { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
