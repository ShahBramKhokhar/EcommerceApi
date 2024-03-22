using WebRexErpAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.Models
{
    public class VisitorMessage : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? CompanyName { get; set; }
        
        public int IndustryQBID { get; set; }
        public int TypeQBID { get; set; }

        public String? Subject { get; set; }
        public String? Street1 { get; set; }
        public String? Street2 { get; set; }
        public String? City { get; set; }
        public String? State { get; set; }
        public String? PostalZip { get; set; }
        public String? Message { get; set; }
        public String? HowCanWeReply { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? VisitorId { get; set; }
       


    }
}
