

using WebRexErpAPI.Business.Category;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService categoryService)
        {

            _CategoryService = categoryService;
        }
        // GET: api/Category/GetAllCategories
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var data = await _CategoryService.GetListAsync();
            if (data == null)
                return NotFound();

            return Ok(data);

        }


        [HttpGet("GetAllCategoryByIndustry/{name}")]
        public ActionResult GetAllCategoryByIndustry([FromRoute] string name)
        {
            var data =  _CategoryService.GetListAsyncByIndustryId(name);
            if (data == null)
                return NotFound();

            return Ok(data);

        }

        [HttpGet("SaveAllCategories")]
        public async Task<IActionResult> SaveAllCategories()
        {
            var data = await _CategoryService.SaveAllCategories();
            if (data == null)
                return NotFound();

            return Ok(data);

        }


    }
}
