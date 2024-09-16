using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class UserType
    {
        [Key]
        public int IdUserType { get; set; }
        public required string UserTypeName { get; set; }

        public required ICollection<User> User { get; set; }
        public required ICollection<PermissionsXUserTypes> PermissionsXUserType { get; set; }
    }
}
