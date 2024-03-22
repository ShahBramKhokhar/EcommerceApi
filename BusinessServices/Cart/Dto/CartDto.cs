using WebRexErpAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.Business.Cart.Dto
{
    public class CartDto
    {

        public int Id { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> Qty { get; set; }
        public double? Price { get; set; }
    }
}
