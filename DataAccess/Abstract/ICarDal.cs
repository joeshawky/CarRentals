using Core.DataAccess;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDto> GetCarsDetails();
        List<CarDto> GetCarsDetailsByBrandId(int brandId);
        List<CarDto> GetCarsDetailsByColorId(int colorId);
        CarDto GetCarDetailsByCarId(int carId);
        List<CarDto> GetCarsDetailsByBrandName(string brandName);
    }
}
