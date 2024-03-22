using WebRexErpAPI.Models.BaseModels;

namespace WebRexErpAPI.Services.Account.Dto
{
    public class UserBindingDto
    {
        public int Id { get; set; } = 0;
        public string? FullName { get; set; } = string.Empty;
        public string? BusinessName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty ;

        public bool? Examet { get; set; } = false;

        public string? FileUrl { get; set; }
        public string? FileName { get; set; }

    }

    public class UserRegisterDto
    {
        public string? FullName { get; set; } = string.Empty;
        public string? BusinessName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int UserTypeId { get;internal set; } 


    }


    public class UserDto
    {
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;


    }

    public class UserVM
    {
        public int Id { get; set; }
        public string? FullName { get; set; } 
        public string? BusinessName { get; set; }
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public bool? Examet { get; set; }
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }

        public int? CustomerKeyQB { get; set; }
        public string? CustomerNameQB { get; set; }
        public int? CustomerQBId { get; set; }
        public bool IsAlreadyRegistor { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;

    }

    public class UserContactDto : BaseQBEntity
    {
        public int UserId { get; set; }
        public int? CustomerKeyQB { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? ContactDivision { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? CustomerName { get;  set; }
        public string? Title { get;  set; }
    }

}
