using WebRexErpAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace WebRexErpAPI.Models
{
    public class ItemImageGallery : BaseQBEntity
    {
        public string? ImageUrl { get; set; }
        public int SortOrder { get; set; }

    }
}
