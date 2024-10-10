using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class PermissionsXUserTypes
    {
        [Key]
        public int IdPermissionsXUserTypes { get; set; }
        public int FKIdPermission { get; set; }
        public int FKIdUserType { get; set; }
        public  UserType UserType { get; set; }
        public  Permissions Permissions { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
