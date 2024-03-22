using WebRexErpAPI.Models;

namespace WebRexErpAPI.Business.SaveLater.Dto
{
    public class SaveLaterDto
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

    }
}
