namespace SabanaBus.DTOs
{
    public class NotificationDto
    {
        public int IDNotification { get; set; }
        public int FkIdSchedule { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
