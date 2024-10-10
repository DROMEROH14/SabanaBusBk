
using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public int FKIdUserType { get; set; }
        public  string Name { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }
        public  UserType UserType { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
