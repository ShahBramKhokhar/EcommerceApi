using WebRexErpAPI.Business.Address;
using WebRexErpAPI.Business.Address.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserAddressController : ControllerBase
    {

        private readonly IUserAddressService _UserAddressService;
        public UserAddressController(IUserAddressService UserAddressService)
        {
            _UserAddressService = UserAddressService;
        }

        [HttpGet("getUserAddressesByUserId/{userId}")]
        public async Task<IActionResult> getUserAddressByUserId([FromRoute] int userId)
        {
            var data = await _UserAddressService.getUserAddresses(userId);
            if (data == null)
                return NotFound();

            return Ok(data);

        }


        [HttpGet("deleteUserAddress/{id}")]
        public async Task<IActionResult> deleteUserAddress([FromRoute] int id)
        {
            var data = await _UserAddressService.DeletAddressAsync(id);
            if (data == false)
                return NotFound();

            return Ok(data);

        }


        [HttpPost("saveUserAddressInfo")]
        public async Task<IActionResult> saveUserAddressInfo([FromBody] UserAddressDto req)
        {
           await _UserAddressService.SaveAddressAsync(req);
           return Ok();

        }




        [HttpPost("updateUserAddressInfo")]
        public async Task<IActionResult> updateUserAddressInfo([FromBody] UserAddressDto req)
        {
            await _UserAddressService.UpdateAddressAsync(req);
            return Ok();

        }


    }
}
