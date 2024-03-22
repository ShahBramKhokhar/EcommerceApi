using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.DataAccess.Models
{
    [Index(nameof(UserId))]
    public class UserContact : BaseQBEntity
    {
        public int UserId { get; set; }
        public int CustomerKeyQB { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? CustomerName { get; set; }
        public string? Title { get; set; }
    }
}
