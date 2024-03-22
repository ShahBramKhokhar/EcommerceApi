using WebRexErpAPI.Business.Industry;
using Microsoft.AspNetCore.Mvc;



namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class IndustryController : ControllerBase
    {

        private readonly IIndustryService _IndustryService;
        public IndustryController(IIndustryService categoryService)
        {

            _IndustryService = categoryService;
        }
        // GET: api/Category/GetAllCategories
        [HttpGet("GetAllIndustry")]
        public async Task<IActionResult> GetAllIndustry()
        {
            var data =  await _IndustryService.GetListAsync();
            if (data == null)
                return NotFound();

            return Ok(data);

        }


        [HttpGet("SaveAllIndustries")]
        public async Task<IActionResult> SaveAllIndustries()
        {
            var data = await _IndustryService.SaveAllIndustries();
            if (data == null)
                return NotFound();

            return Ok(data);

        }


    }
}
