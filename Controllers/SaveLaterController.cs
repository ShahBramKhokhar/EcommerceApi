using WebRexErpAPI.Business.SaveLater;
using WebRexErpAPI.Business.SaveLater.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SaveLaterController : ControllerBase
    {

        private readonly ISaveLaterService _SaveLaterService;
        public SaveLaterController(ISaveLaterService SaveLaterService)
        {
            _SaveLaterService = SaveLaterService;
        }

        [HttpGet("getSaveLatersInfoByUserId/{userId}")]
        public async Task<IActionResult> getSaveLatersInfoByUserId([FromRoute] int userId)
        {
            var data = await _SaveLaterService.getUserSaveLater(userId);
            if (data == null)
                return NotFound();
            return Ok(data);

        }

        [HttpGet("deleteSaveLater/{id}")]
        public async Task<IActionResult> deleteSaveLater([FromRoute] int id)
        {
            var data = await _SaveLaterService.DeletSaveLaterAsync(id);
            if (data == false)
                return NotFound();
            return Ok(data);

        }


        [HttpPost("saveSaveLaterInfo")]
        public async Task<IActionResult> saveSaveLaterInfo([FromBody] SaveLaterDto req)
        {
           await _SaveLaterService.SaveSaveLaterAsync(req);
           return Ok();

        }


    }
}
