using Business.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success) return BadRequest(userToLogin);


            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success) return BadRequest(result);

            var newSuccessDataResult = new SuccessDataResult<AccessToken>(result.Data, result.Message);
            return Ok(newSuccessDataResult);

        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto);
            if (!registerResult.Success) return BadRequest(registerResult) ;

            var result = _authService.CreateAccessToken(registerResult.Data);
            if (!result.Success) return BadRequest(result);

            var newSuccesDataResult = new SuccessDataResult<AccessToken>(result.Data, result.Message);
            return Ok(newSuccesDataResult);
        }

        [HttpPost("updatePassword")]
        public IActionResult UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            var result = _authService.UpdatePassword(updatePasswordDTO);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
