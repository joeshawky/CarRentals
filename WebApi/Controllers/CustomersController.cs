using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _customerServicve;

        public CustomersController(ICustomerService customerService)
        {
            _customerServicve = customerService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _customerServicve.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Customer customer)
        {
            var result = _customerServicve.AddCustomer(customer);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Customer customer)
        {
            var result = _customerServicve.Update(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = _customerServicve.Delete(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
