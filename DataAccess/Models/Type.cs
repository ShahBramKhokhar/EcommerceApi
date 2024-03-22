using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.Models
{
    [Index(nameof(CategoryId))]
    [Index(nameof(ItemCount))]
    public class Type  : BaseQBEntity
    {
        public string? Name { get; set; }
        public int ItemCount { get; set; }
        public int? CategoryId { get; set; }

    }
}
