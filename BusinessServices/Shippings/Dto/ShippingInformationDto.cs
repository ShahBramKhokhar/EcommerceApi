using WebRexErpAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.Business.ShippingInformation.Dto
{
    public class ShippingInformationDto
    {
        public int? Id { get; set; }
        public string? NameAlias { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip_PostalCode { get; set; }
        public string? Country { get; set; }

        public int? UserId { get; set; }
        
    }
}
