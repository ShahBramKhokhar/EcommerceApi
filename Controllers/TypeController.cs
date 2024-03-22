

using WebRexErpAPI.Business.Type;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class TypeController : ControllerBase
    {

        private readonly ITypeService _typeService;
        public TypeController(ITypeService TypeService)
        {

            _typeService = TypeService;
        }
        // GET: api/Type/GetAllCategories
        [HttpGet("GetAllType/{categoryId}")]
        public async Task<IActionResult> GetAllType([FromRoute]  int categoryId = 0)
        {

            var data = await _typeService.GetListAsync(categoryId);
            if (data == null)
                return NotFound();

            return Ok(data);

        }



    }
}
