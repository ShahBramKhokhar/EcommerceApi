using System.ComponentModel.DataAnnotations;

namespace WebRexErpAPI.Models.BaseModels
{
    public class BaseQBEntity
    {
        [Key]
        public int Id { get; set; }
        public int QbRecordId { get; set; }
    }
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
    }
}
