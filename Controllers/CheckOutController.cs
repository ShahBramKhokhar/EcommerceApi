

using WebRexErpAPI.Business.CheckOut;
using WebRexErpAPI.BusinessServices.CheckOut.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CheckOutController : ControllerBase
    {

      private readonly ICheckOutService _checkOutService;
        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }
     
        [HttpPost("checkout")]
        public async Task<IActionResult> GetAllCheckOutAsync([FromBody] CheckoutInputModel input)
        {
            var data =   await _checkOutService.SaveCheckOutAsync(input);
            return Ok(data);

        }




    }
}
