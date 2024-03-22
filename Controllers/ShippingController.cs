using WebRexErpAPI.Business.ShippingInformation;
using WebRexErpAPI.Business.ShippingInformation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ShippingInformationController : ControllerBase
    {

        private readonly IShippingInformationService _ShippingInformationService;
        public ShippingInformationController(IShippingInformationService shippingInformationService)
        {
            _ShippingInformationService = shippingInformationService;
        }

        [HttpGet("getShippingsInfoByUserId/{userId}")]
        public async Task<IActionResult> getShippingsInfoByUserId([FromRoute] int userId)
        {
            var data = await _ShippingInformationService.getUserShippings(userId);
            if (data == null)
                return NotFound();

            return Ok(data);

        }

        [HttpGet("deleteShipping/{id}")]
        public async Task<IActionResult> deleteShipping([FromRoute] int id)
        {
            var data = await _ShippingInformationService.DeletShippingInformationAsync(id);
            if (data == false)
                return NotFound();

            return Ok(data);

        }


        [HttpPost("saveShippingInfo")]
        public async Task<IActionResult> saveShippingInfo([FromBody] ShippingInformationDto req)
        {
           var data =  await _ShippingInformationService.SaveShippingInformationAsync(req);
            if(data == null)
                return Ok(null);
           return Ok(data);

        }



        [HttpPost("updateShippingInfo")]
        public async Task<IActionResult> updateShippingInfo([FromBody] ShippingInformationDto req)
        {
            await _ShippingInformationService.UpdateShippingInformationAsync(req);
            return Ok();

        }


    }
}
