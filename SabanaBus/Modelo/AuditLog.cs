using System.ComponentModel.DataAnnotations;

namespace SabanaBus.Modelo

{
    public class AuditLog
    {
   
        public int Id { get; set; }

        public  string EntityName { get; set; }

        public int EntityId { get; set; }
   
        public  string ActionType { get; set; }
 
        public  string OldValues { get; set; }
 
        public string NewValues { get; set; }
        public DateTime Date { get; set; }

        public string ModifiedBy { get; set; }
    }
}
