using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult AddCarImage(int carId, IFormFile imageFile);
        IResult UpdateCarImage(CarImage carImage, IFormFile imageFile);
        IResult RemoveCarImage(CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
    }
}
