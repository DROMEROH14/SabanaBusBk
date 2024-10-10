namespace SabanaBus.DTOs
{
    public class RouteDTO
    {
        public int IdRoute { get; set; }
        public string RouteName { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public bool IsDeleted { get; set; }
    }
}
