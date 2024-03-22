using WebRexErpAPI.Business.Email.Dto;
using WebRexErpAPI.BusinessServices.CheckOut.Dto;

namespace WebRexErpAPI.Business.Email
{
    public interface IEmailService
    {
        Task<HttpResponseMessage> OrderConfirmationEmail(CheckoutInputModel input);
        Task<bool> EmailUsingAzureLogicApp(EmailDto emailContent);



    }
}
