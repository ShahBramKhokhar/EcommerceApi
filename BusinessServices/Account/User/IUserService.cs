using WebRexErpAPI.BusinessServices.Account.Dto;
using WebRexErpAPI.Common.CommonDto;
using WebRexErpAPI.Services.Account.Dto;

namespace WebRexErpAPI.Services.Account.User
{
    public interface IUserService
    {
        string GetUserEmailClaime();
        Task<UserVM?> Register(UserRegisterDto request);
        Task<UserVM?> Login(UserDto request);
        Task<UserVM?> UpdateUserInfo(UserBindingDto request);
        Task<bool> UpdateFileUrlUser(UserBindingDto request);
        Task<UserVM> isUserExit(string email);
        Task<bool> ResetPasswordRequest(string email);
        Task<HttpResponseMessage> ConfirmResetPassword(PasswordResetModel input);



    }
}
