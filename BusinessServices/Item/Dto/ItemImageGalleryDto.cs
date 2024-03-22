using WebRexErpAPI.Models.BaseModels;

namespace WebRexErpAPI.Services.Item.Dto
{
    public class ItemImageGalleryDto 
    {
        public string original { get; set; }
        public string thumbnail { get; set; }
        public int SortOrder { get; set; }
    }
}
