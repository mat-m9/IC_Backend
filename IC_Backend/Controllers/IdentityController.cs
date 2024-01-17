using IC_Backend.ResponseModels;
using IC_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace IC_Backend.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(template: ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var authResponse = await _identityService.RegisterAsync(request.userName, request.password, request.userMail, request.phone, "comprador");


            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                }); ;
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(template: ApiRoutes.Identity.RegisterVendedor)]
        public async Task<IActionResult> RegisterVendedor([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var authResponse = await _identityService.RegisterAsync(request.userName, request.password, request.userMail, request.phone, "vendedor");


            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                }); ;
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(template: ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.userMail, request.password);

            if (!authResponse.Success)
            {
                return BadRequest();
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }


        [HttpPut(ApiRoutes.Identity.Change)]
        public async Task<IActionResult> ChangePassWord([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            bool response = await _identityService.ChangePassword(
                changePasswordRequest.UserId, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            if (response)
                return NoContent();
            return BadRequest();
        }
    }
}
