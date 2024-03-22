using Microsoft.AspNetCore.Mvc;
using WebRexErpAPI.Services.Account.Dto;
using WebRexErpAPI.Services.Account.User;

namespace WebRexErpAPI.Controllers
{
    public class Admin : Controller
    {
        private readonly UserServices _userServices;

        public Admin(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("loginAdmin")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {
            try
            {
                var result = await _userServices.Login(request);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Login successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Invalid email or password" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request" });
            }
        }
    }
}
