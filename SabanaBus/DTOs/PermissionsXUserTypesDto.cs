namespace SabanaBus.DTOs
{
    public class PermissionsXUserTypesDto
    {
        public int IdPermissionsXUserTypes { get; set; }
        public int FKIdPermission { get; set; }
        public int FKIdUserType { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
