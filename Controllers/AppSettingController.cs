

using WebRexErpAPI.Business.Email;
using WebRexErpAPI.BusinessServices.CheckOut.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class AppSettingController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AppSettingController(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        // GET: api/AppSetting/GetAllCategories
        [HttpGet("appsettings")]
        public async Task<IActionResult> GetStripePublicKey()
        {
            var publickeyStrip =  Content(_configuration.GetValue<string>("Stripe:Publishablekey"), "text/plain");

            Dictionary<string, string> Settings = new Dictionary<string, string>();
            Settings.Add("PublicKeyStrip", publickeyStrip.Content);

            return Ok(Settings);

        }

        //[HttpPost("SendTestEmail")]
        //public async Task<IActionResult> SendTestEmail(CheckoutInputModel input)
        //{
        // var IsSend = await _emailService.AzureLogicEmail(input);

        //    return Ok(IsSend);
        //}


        //[HttpPost("Testpath")]
        //public async Task<IActionResult> Testpath(CheckoutInputModel input)
        //{
        //    var data = await _emailService.RenderRazorViewToString(input);

        //    return Ok(data);
        //}



    }
}
