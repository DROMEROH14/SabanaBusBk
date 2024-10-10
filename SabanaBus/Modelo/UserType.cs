using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class UserType
    {
        [Key]
        public int IdUserType { get; set; }
        public  string UserTypeName { get; set; }

        public List<User> User { get; set; } = new List<User>();

        public List<PermissionsXUserTypes> PermissionsXUserType { get; set; } = new List<PermissionsXUserTypes>();
   

        public bool IsDeleted { get; set; } 
    }
}
