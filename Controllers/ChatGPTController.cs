

using WebRexErpAPI.Business.ChatGPT;
using WebRexErpAPI.BusinessServices.ChatGPT.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class ChatGPTController : ControllerBase
    {

        private readonly IChatGPTService _ChatGPTService;
        public ChatGPTController(IChatGPTService ChatGPTService)
        {

            _ChatGPTService = ChatGPTService;
        }
        // GET: api/ChatGPT/GenerateContent
        //[HttpPost("ChatGPT/GenerateContent")]
        //public async Task<IActionResult> GetAllChatGPT([FromBody] ChatGPTCompletionsDto input)
        //{

        //    var data = await _ChatGPTService.GenerateContentTubro(input);
        //    if (data == null)
        //        return NotFound();

        //    return Ok(data);

        //}

        // GET: api/ChatGPT/GenerateContentGPT4
       // [HttpPost("ChatGPT/GenerateContentGPT4")]
        //public async Task<IActionResult> GenerateContentGPT4([FromBody] ChatGPTCompletionsDto input)
        //{

        //    var data = await _ChatGPTService.GenerateContentGPT4Async(input);
        //    if (data == null)
        //        return NotFound();

        //    return Ok(data);

        //}

    }
}
