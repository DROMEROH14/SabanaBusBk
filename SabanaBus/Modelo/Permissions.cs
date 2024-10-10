using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo
{
    public class Permissions
    {
        [Key]
        public int IdPermission { get; set; }
        public string Permission { get; set; }

        public List<PermissionsXUserTypes> PermissionsXUserType { get; set; } = new List<PermissionsXUserTypes>();

  

        public bool IsDeleted { get; set; } = false;
    }
}
