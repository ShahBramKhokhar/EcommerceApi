namespace WebRexErpAPI.BusinessServices.Account.Dto
{
    public class PasswordResetModel
    {
        public string? Email { get; set; }
        public string? OTP { get; set; }
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
