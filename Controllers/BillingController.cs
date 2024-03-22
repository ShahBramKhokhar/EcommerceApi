using WebRexErpAPI.Business.Billing;
using WebRexErpAPI.Business.Billing.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BillingController : ControllerBase
    {

        private readonly IBillingService _BillingService;
        public BillingController(IBillingService BillingService)
        {
            _BillingService = BillingService;
        }

        [HttpGet("getBillingsInfoByUserId/{userId}")]
        public async Task<IActionResult> getBillingsInfoByUserId([FromRoute] int userId)
        {
            var data = await _BillingService.getUserBilling(userId);
            if (data == null)
                return NotFound();

            return Ok(data);

        }

        [HttpGet("deleteBilling/{id}")]
        public async Task<IActionResult> deleteBilling([FromRoute] int id)
        {
            var data = await _BillingService.DeletBillingAsync(id);
            if (data == false)
                return NotFound();

            return Ok(data);

        }


        [HttpPost("saveBillingInfo")]
        public async Task<IActionResult> saveBillingInfo([FromBody] BillingDto req)
        {
           var data =  await _BillingService.SaveBillingAsync(req);
            if (data == null)
                return Ok(null);
            return Ok(data);

        }


        [HttpPost("updateBillingInfo")]
        public async Task<IActionResult> updateBillingInfo([FromBody] BillingDto req)
        {
            await _BillingService.UpdateBillingAsync(req);
            return Ok();

        }


    }
}
