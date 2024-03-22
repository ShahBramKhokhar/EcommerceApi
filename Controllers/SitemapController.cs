

using WebRexErpAPI.Business.Sitemap;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class SitemapController : ControllerBase
    {
        private readonly ISitemapService _sitemapService;
        public SitemapController(ISitemapService sitemapService)
        {
            _sitemapService = sitemapService;
        }
        
        
        [HttpGet("getSiteMap")]
        public async Task<IActionResult> getSiteMap()
        {
            var sitemap = await _sitemapService.GenerateAndUploadSitemap();
           return Ok(sitemap);
        }


    }
}
