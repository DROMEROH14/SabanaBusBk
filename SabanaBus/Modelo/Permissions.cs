using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Permissions
    {
        [Key]
        public int IdPermission { get; set; }
        public required string Permission { get; set; }
        public required ICollection<PermissionsXUserTypes> PermissionsXUserType { get; set; }
    }
}
