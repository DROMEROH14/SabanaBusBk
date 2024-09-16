using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public int FKIdUserType { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }
        public required UserType UserType { get; set; }
    }
}
