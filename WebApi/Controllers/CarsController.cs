using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPost("Add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("GetCarsDetails")] 
        public IActionResult GetCarsDetails()
        {
            var result = _carService.GetCarsDetails();

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("GetCarDetailsByCarId")]
        public IActionResult GetCarDetailsByCarId(int carId)
        {
            var result = _carService.GetCarDetailsByCarId(carId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarsDetailsByBrandName")]
        public IActionResult GetCarsDetailsByBrandName(string brandName)
        {
            var result = _carService.GetCarsDetailsByBrandName(brandName);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet("GetCarsDetailsByBrandId")]
        public IActionResult GetCarsDetailsByBrandId(int brandId)
        {
            var result = _carService.GetCarsDetailsByBrandId(brandId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarsDetailsByColorId")]
        public IActionResult GetCarsDetailsByColorId(int colorId)
        {
            var result = _carService.GetCarsDetailsByColorId(colorId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarsByBrandId")]
        public IActionResult Getcarsbybrandid(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("GetCarsByColorId")]
        public IActionResult Getcarsbycolorid(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("GetByCarId")]
        public IActionResult Getbycarid(int carId)
        {
            var result = _carService.GetById(carId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
