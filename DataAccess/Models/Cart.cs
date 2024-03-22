using WebRexErpAPI.Models.BaseModels;

namespace WebRexErpAPI.Models
{
    public class Cart : BaseEntity
    {
       
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> Qty { get; set; }
        public double? Price { get; set; }


    }
}
