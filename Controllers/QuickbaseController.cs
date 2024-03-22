
using WebRexErpAPI.Services.Common;
using WebRexErpAPI.Services.QuickBase;
using Microsoft.AspNetCore.Mvc;


namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class QuickBaseController : ControllerBase
    {

        private readonly IQuickBaseService _quickbaseService;
        public QuickBaseController(IQuickBaseService quickbaseService)
        {
            _quickbaseService = quickbaseService;
        }
        [HttpGet("updatedQb/{id}")]
        public async Task<IActionResult> updatedQb([FromRoute]  int id )
        {

            var data = await _quickbaseService.GetQBToItem(id);
            if (data != true)
                return NotFound();

            return Ok(data);
        }


        [HttpGet("ChatGPTAppraisal/{id}")]
        public async Task<IActionResult> ChatGPTAppraisal([FromRoute] int id)
        {

            var data = await _quickbaseService.GetQBItemCheckAppraiseChatGPT(id);
            if (data != true)
                return NotFound();

            return Ok(data);
        }

        [HttpPost("findCustomerBusinessQB")]
        public async Task<IActionResult> findCustomerBusinessQB([FromBody] string businessName)
        {
            var data = await _quickbaseService.FindCustomerQBBusiness(businessName);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
          }

        [HttpPost("findCustomerContacts")]
        public async Task<IActionResult> findCustomerContacts(int customerNumber)
        {
           var data =  await _quickbaseService.FindCustomerContacts(customerNumber);
            if(data == null)
            {
                return NotFound();
            }
            else {
                return Ok(data);
            }
        }

        [HttpPost("findCustomerSale")]
        public async Task<IActionResult> findCustomerSale(int recordId)
        {
            var data = await _quickbaseService.FindCustomerSaleQB(recordId);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPost("createQBContact")]
        public async Task<IActionResult> CreateContact(ContactQBDto input)
        {
            var data = await _quickbaseService.CreateQBContact(input);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

    }
}
