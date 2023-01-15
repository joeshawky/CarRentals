using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();

            if (result.Success == false)
                return BadRequest(result);

            if (result.Data.Count == 0)
            {

                return Ok(new SuccessDataResult<List<CarImage>>(new List<CarImage> { 
                    new CarImage { 
                        ImagePath = DefaultImages.DefaultCarImageUrl
                    } 
                }));
            }

            return Ok(result);


        }


        [HttpPost("Add")]
        public IActionResult Add(int carId, IFormFile imageFile)
        {
            var result = _carImageService.AddCarImage(carId, imageFile);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] CarImage carImage, IFormFile imageFile)
        {
            var result = _carImageService.UpdateCarImage(carImage, imageFile);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(CarImage carImage)
        {
            var result = _carImageService.RemoveCarImage(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



    }
}
