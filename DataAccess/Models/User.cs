
using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using WebRexErpAPI.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.Models
{
    [Index(nameof(Email))]
    public class User :BaseEntity
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? BusinessName { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool? Examet { get; set; }
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }
        public int? CustomerKeyQB { get; set; }
        public string? CustomerNameQB { get; set; }
        public int? CustomerQBId { get;  set; }
        public string? OtpHash { get;  set; }
        public DateTime? OtpExpiration { get;  set; }

       // [ForeignKey(nameof(UserTypeId))]
          public int UserTypeId { get; set; }
      //   public UserType UserType { get; set; }
    }
}
