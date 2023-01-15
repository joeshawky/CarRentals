using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<Car> GetById(int id);
        IDataResult<List<Car>> GetAll(Func<Car, bool>? filter = null);
        IResult DeleteById(int carId);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<List<CarDto>> GetCarsDetails();
        IDataResult<List<CarDto>> GetCarsDetailsByBrandId(int brandId);
        IDataResult<List<CarDto>> GetCarsDetailsByColorId(int colorId);
        IDataResult<CarDto> GetCarDetailsByCarId(int carId);
        IDataResult<List<CarDto>> GetCarsDetailsByBrandName(string brandName);

    }
}
