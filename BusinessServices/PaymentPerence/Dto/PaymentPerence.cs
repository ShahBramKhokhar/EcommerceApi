using WebRexErpAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.Business.PaymentPreference.Dto
{
    public class PaymentPreferenceDto
    {
        public int? Id { get; set; }
        public string? NameAlias { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? CardLastDigits { get; set; }
        public string? ExpireDate { get; set; }
        public string? CardType { get; set; }
        public int? UserId { get; set; }

    }
}
