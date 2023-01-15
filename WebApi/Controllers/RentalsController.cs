using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetAllDetails")]
        public IActionResult GetAllDetails()
        {
            var result = _rentalService.GetAllDetails();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetDetailsByCarId")]
        public IActionResult GetDetailsByCarId(int carId)
        {
            var result = _rentalService.GetDetailsByCarId(carId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.AddRental(rental);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Rental rental)
        {
            var result = _rentalService.Update(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int rentalId)
        {
            var result = _rentalService.Delete(rentalId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
