using WebRexErpAPI.BusinessServices.ShipEngine;
using WebRexErpAPI.BusinessServices.ShipEngine.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ShipEngineController : ControllerBase
    {
        private readonly IShipEngineService _ShipEngineService;
        public ShipEngineController(IShipEngineService ShipEngineService)
        {
            _ShipEngineService = ShipEngineService;
        }

        [HttpPost("getPostalShipRates")]
        public async Task<IActionResult> getShipRates([FromBody] SERequest req)
        {
            var data = await _ShipEngineService.GetRates(req);
            if (data == null)
                return NotFound();
            return Content(data, "application/json");

        }

        [HttpPost("getRatesTLT")]
        public async Task<IActionResult> GetRatesTLT([FromBody] SELTLRequest req)
        {
            var data = await _ShipEngineService.GetRatesTLT(req);
            if (data == null)
                return NotFound();
            return Content(data, "application/json");

        }

        [HttpGet("listCarriers")]
        public async Task<IActionResult> ListCarriers()
        {
            var data = await _ShipEngineService.ListCarriersAsync();
            if (data == null)
                return NotFound();
            return Ok(data);

        }

        //[HttpPost("validateAddress")]
        //public async Task<IActionResult> validateAddress([FromBody] SERequestAddress req)
        //{
        //    var data = await _ShipEngineService.ValidateAddress(req);
        //    if (data == null)
        //        return NotFound();
        //    return Ok(data);

        //}


        [HttpGet("getODFL")]
        public async Task<IActionResult> getOldDominionFreightLines()
        {
            var data = await _ShipEngineService.ODFL();
            if (data == null)
                return NotFound();
            return Ok(data);

        }



        [HttpGet("getSAIA")]
        public async Task<IActionResult> SAIA()
        {
            var data = await _ShipEngineService.SAIA();
            if (data == null)
                return NotFound();
            return Ok(data);

        }

        [HttpGet("getXPOID")]
        public async Task<IActionResult> XPOID()
        {
            var data = await _ShipEngineService.XPOID();
            if (data == null)
                return NotFound();
            return Ok(data);

        }

        [HttpGet("TLTCarrierSupport/{carrierId}")]
        public async Task<IActionResult> TLTCarrierSupported([FromRoute] string carrierId)
        {
            var data = await _ShipEngineService.TLTCarrierSupported(carrierId);
            if (data == null)
                return NotFound();
            return Ok(data);

        }

        [HttpPost("validateAddress")]
        public async Task<IActionResult> ValidateAddress(SEAddressDTO address)
        {
            var result = await _ShipEngineService.ValidateAddressAsync(address);
            return Ok(result);
        }



    }
}
