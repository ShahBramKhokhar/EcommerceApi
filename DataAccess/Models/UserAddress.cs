using WebRexErpAPI.Models;
using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.Models
{
    [Index(nameof(UserId))]
    public class UserAddress : BaseEntity
    {

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
        public int UserId { get; set; }

    }
}
