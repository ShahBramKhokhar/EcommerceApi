using WebRexErpAPI.Business.PaymentPreference;
using WebRexErpAPI.Business.PaymentPreference.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PaymentPreferenceController : ControllerBase
    {

        private readonly IPaymentPreferenceService _PaymentPreferenceService;
        public PaymentPreferenceController(IPaymentPreferenceService PaymentPreferenceService)
        {
            _PaymentPreferenceService = PaymentPreferenceService;
        }

        [HttpGet("getPaymentPreferencesInfoByUserId/{userId}")]
        public async Task<IActionResult> getPaymentPreferencesInfoByUserId([FromRoute] int userId)
        {
            var data = await _PaymentPreferenceService.getUserPaymentPreference(userId);
            if (data == null)
                return NotFound();

            return Ok(data);

        }

        [HttpGet("deletePaymentPreference/{id}")]
        public async Task<IActionResult> deletePaymentPreference([FromRoute] int id)
        {
            var data = await _PaymentPreferenceService.DeletPaymentPreferenceAsync(id);
            if (data == false)
                return NotFound();

            return Ok(data);

        }


        [HttpPost("savePaymentPreferenceInfo")]
        public async Task<IActionResult> savePaymentPreferenceInfo([FromBody] PaymentPreferenceDto req)
        {
          var data =  await _PaymentPreferenceService.SavePaymentPreferenceAsync(req);
          if (data == null)
                return NotFound();

          return Ok(data);


        }


        //[HttpPost("updatePaymentPreferenceInfo")]
        //public async Task<IActionResult> updatePaymentPreferenceInfo([FromBody] PaymentPreferenceDto req)
        //{
        //    await _PaymentPreferenceService.UpdatePaymentPreferenceAsync(req);
        //    return Ok();

        //}


    }
}
