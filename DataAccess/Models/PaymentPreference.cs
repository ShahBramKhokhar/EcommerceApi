using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.Models
{
    [Index(nameof(UserId))]
    public class PaymentPreference : BaseEntity
    {
        public string? NameAlias { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? CardLastDigits { get; set; }
        public string? ExpireDate { get; set; }
        public string? CardType { get; set; }
        public int? UserId { get; set; }

    }
}
