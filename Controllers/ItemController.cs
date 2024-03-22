using WebRexErpAPI.Business.Industry;
using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Common.CommonDto;
using WebRexErpAPI.Models;
using WebRexErpAPI.Services.Pagednation;
using WebRexErpAPI.Services.QuickBase;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRexErpAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api/")]
    public class ItemController : ControllerBase
    {

        #region private veriables 

        private readonly IItemService _itemService;
        
        #endregion
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
            
        }
    

        [HttpGet("GetItemImageGallery/{qucikBaseId}")]
        public async Task<IActionResult> GetItemImageGallery([FromRoute] int qucikBaseId)
        {
            var data = await _itemService.ItemGalleryByQBIdAsync(qucikBaseId);
            if (data == null)
                return NotFound();

            return Ok(data);
           
        }


        [HttpGet("GetRecentItems")]
        public async Task<IActionResult> GetRecentItems()
        {
            var data = await _itemService.GetRecentItemListAsync();
            if(data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost("GetPagedItems")]
        public async Task<IActionResult> GetPagedItems([FromBody] PaginationFilter filter)
        {
            var data = await _itemService.PagedItemsWithGlobalSearch(filter);
            if (data == null)
                return NotFound();

            return Ok(data);

        }



        [HttpGet("GetItemById/{id}")]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            var data = await _itemService.GetItemById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }



        [HttpGet("GetRandomItemByCategory/{categoryId}")]
        public async Task<IActionResult> GetRandomItemByCategory([FromRoute] int categoryId)
        {
            var data = await _itemService.getRandomItemByCategory(categoryId);
            if (data == null)
                return NotFound();

            return Ok(data);

        }


        [HttpGet("GetAttempts{itemId}")]
        public  ActionResult<int> GetAttempts(int itemId)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            return  _itemService.GetAttempts(ipAddress, itemId);
        }

        [HttpPost("AddAttemptAsync{itemId}")]
        public async Task<ActionResult> AddAttemptAsync(int itemId)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            await _itemService.AddAttemptAsync(ipAddress, itemId);
            return Ok();
        }

        [HttpGet("TotalItemCount")]
        public  ActionResult TotalItemCount()
        {
            var data =  _itemService.TotalItemCount();
            return Ok(data);

        }

        [HttpPost("insert")]
        public async Task<ActionResult<ItemDto>> InsertItem([FromBody] ItemDto newItem)
        {
            try
            {
                var insertedItem = await _itemService.InsertItemAsync(newItem);
                return Ok(insertedItem);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<ItemDto>> UpdateItem([FromBody] ItemDto updatedItem)
        {
            try
            {
                var updatedItemResult = await _itemService.UpdateItemAsync(updatedItem);
                return Ok(updatedItemResult);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
