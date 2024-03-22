using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.Models
{
    [Index(nameof(Name))]
    [Index(nameof(ItemCount))]
    public class Industry : BaseQBEntity
    {
        public string? Name { get; set; }
        public int ItemCount { get; set; }
    }
}
