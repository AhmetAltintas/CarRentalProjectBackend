using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        IRentService _rentService;

        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost("add")]
        public IActionResult Add(Rent rent)
        {
            var result = _rentService.Add(rent);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Update(Rent rent)
        {
            var result = _rentService.Update(rent);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("delete")]
        public IActionResult Delete(Rent rent)
        {
            var result = _rentService.Delete(rent);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getrentdetails")]
        public IActionResult GetRentDetails()
        {
            var result = _rentService.GetRentDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        
        [HttpGet("getrentdetailsbycarid")]
        public IActionResult GetRentDetailsByCarId(int carId)
        {
            var result = _rentService.GetRentDetailsByCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
