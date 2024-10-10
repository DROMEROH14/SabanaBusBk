namespace SabanaBus.DTOs
{
    public class PermissionsDto
    {
        public int IdPermission { get; set; }
        public string Permission { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
