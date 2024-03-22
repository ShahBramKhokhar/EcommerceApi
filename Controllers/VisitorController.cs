using WebRexErpAPI.Services.VisitorMessage;
using WebRexErpAPI.Services.VisitorMessage.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorMessageService _visitorMessageService;
        public VisitorController(IVisitorMessageService visitorMessageService)
        {
            _visitorMessageService = visitorMessageService;

        }

        [HttpPost("SaveVisitorMessage")]
        public async Task<IActionResult> SaveVisitorMessage([FromBody] VisitorMessageDto input )
        {
            var data = await  _visitorMessageService.CreateAsync(input);
            if (!data)
                return NotFound();

            return Ok(data);

        }
    }
}
