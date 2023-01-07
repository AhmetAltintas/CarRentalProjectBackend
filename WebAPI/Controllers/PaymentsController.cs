using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _paymentService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("pay")]
        public IActionResult Pay(Payment payment)
        {
            var result = _paymentService.Pay(payment);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Payment payment)
        {
            var result = _paymentService.Add(payment);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Payment payment)
        {
            var result = _paymentService.Update(payment);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Payment payment)
        {
            var result = _paymentService.Delete(payment);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _paymentService.GetById(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getAllByCustomerId")]
        public IActionResult GetAllByCustomerId(int customerId)
        {
            var result = _paymentService.GetAllByCustomerId(customerId);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("checkIfThisCardIsAlreadySavedForThisCustomer")]
        public IActionResult CheckIfThisCardIsAlreadySavedForThisCustomer(Payment payment)
        {
            var result = _paymentService.CheckIfThisCardIsAlreadySavedForThisCustomer(payment);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

    }
}
