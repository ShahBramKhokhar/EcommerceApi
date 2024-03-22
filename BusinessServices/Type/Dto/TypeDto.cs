using System.ComponentModel.DataAnnotations;

namespace WebRexErpAPI.Business.Type.Dto
{
    public class TypeDto
    {
        public int? Id { get; set; } = 0;
        public string Name { get; set; }
        //public int QbRecordId { get; set; }
        //public int? CategoryId { get; set; }
        public int ItemCount { get; internal set; }
    }
}
