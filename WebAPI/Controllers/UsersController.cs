using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getDTOById")]
        public IActionResult GetDTOById(int id)
        {
            var result = _userService.GetDTOById(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
