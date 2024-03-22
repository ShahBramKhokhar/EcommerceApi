using WebRexErpAPI.BusinessServices.Account.Dto;
using WebRexErpAPI.Services.Account.Dto;
using WebRexErpAPI.Services.Account.User;
using Microsoft.AspNetCore.Mvc;

namespace WebRexErpAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]  UserRegisterDto request)
        {
            request.UserTypeId = 1;
            var data =await _userService.Register(request);
            return Ok(data);
        }

        [HttpPost("RegisterAmin")]
        public async Task<IActionResult> RegisterAmin([FromBody] UserRegisterDto request)
        {
            request.UserTypeId = 2;
            var data = await _userService.Register(request);
            return Ok(data);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {

            var data = await _userService.Login(request);
            if (data == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                return Ok(data);
            }
          
        }
        [HttpPost("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserBindingDto request)
        {
            var data = await _userService.UpdateUserInfo(request);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost("UpdateFileUrlUser")]
        public async Task<IActionResult> UpdateFileUrlUser([FromBody] UserBindingDto request)
        {
            var data = await _userService.UpdateFileUrlUser(request);
            if (data)
            {
                return Ok(data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost("CheckUserExist")]
        public async Task<IActionResult> CheckUserExist([FromBody] string email)
        {
            var data = await _userService.isUserExit(email);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);

            }
        }


        [HttpGet("ResetPasswordRequest/{email}")]
        public async Task<IActionResult> ResetPasswordRequest([FromRoute] string email)
        {
            var data = await _userService.ResetPasswordRequest(email);
            return Ok(data);
         
        }


        [HttpPost("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword(PasswordResetModel input)
        {
            var data = await _userService.ConfirmResetPassword(input);
            return Ok(data);
        }
      


    }
}
