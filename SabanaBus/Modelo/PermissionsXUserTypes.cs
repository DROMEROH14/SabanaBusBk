using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class PermissionsXUserTypes
    {
        [Key]
        public int IdPermissionsXUserTypes { get; set; }
        public int FKIdPermission { get; set; }
        public int FKIdUserType { get; set; }
        public required UserType UserType { get; set; }
        public required Permissions Permissions { get; set; }

    }
}
