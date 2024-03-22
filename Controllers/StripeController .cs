


using WebRexErpAPI.BusinessServices.Stripe;
using WebRexErpAPI.BusinessServices.Stripe.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class StripeController : ControllerBase
    {

       
        public StripeController( )
        {
           
        }
        // GET: api/Stripe/GetAllCategories
        //[HttpPost("StripCreateIntent")]
        //public async Task<IActionResult> StripCreateIntent([FromBody] PaymentIntentDto input)
        //{
        //    var data = await _StripeService.CreatePaymentIntent(input);
        //    if (data == null || data == string.Empty)
        //        return NotFound();

        //    return Ok(data);

        //}


        //[HttpPost("getPaid")]
        //public async Task<IActionResult> StripCreateIntent()
        //{
        //    var data = await _StripeService.ProcessPaymentAsync();
        //    if (data == null || data == string.Empty)
        //        return NotFound();

        //    return Ok(data);

        //}





    }
}
