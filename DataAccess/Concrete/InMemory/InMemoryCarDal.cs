using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
                new Car() {Id = 1, BrandId = 2, ColorId = 1, DailyPrice = 25000, ModelYear = 1998},
                new Car() {Id = 2, BrandId = 1, ColorId = 2, DailyPrice = 50000, ModelYear = 2015},
                new Car() {Id = 3, BrandId = 3, ColorId = 2, DailyPrice = 100000, ModelYear = 2003},
                new Car() {Id = 4, BrandId = 1, ColorId = 3, DailyPrice = 10000, ModelYear = 2005},
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var carToBeDeleted = _cars.SingleOrDefault(c => c.Id == car.Id);
            if (carToBeDeleted is null)
            {
                Console.WriteLine("Car was not found.");
                return;
            }
            _cars.Remove(carToBeDeleted);
        }

        public Car? Get(Func<Car, bool> filter)
        {
            return _cars.SingleOrDefault(filter);
        }

        public List<Car> GetAll(Func<Car, bool>? filter = null)
        {
            return filter is null ? _cars : _cars.Where(filter).ToList();
        }

        public CarDto GetCarDetailsByCarId(int carId)
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsDetails(Func<Car, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsDetails()
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsDetailsByBrandId(int brandId)
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsDetailsByBrandName(string brandName)
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsDetailsByCarId(int carId)
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsDetailsByColorId(int colorId)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            var carToBeUpdated = _cars.SingleOrDefault(c => c.Id == car.Id);
            if (carToBeUpdated is null)
            {
                Console.WriteLine("Car was not found");
                return;
            }


            carToBeUpdated.Id = car.Id;
            carToBeUpdated.BrandId = car.BrandId;
            carToBeUpdated.ColorId = car.ColorId;
            carToBeUpdated.DailyPrice = car.DailyPrice;
            carToBeUpdated.ModelYear = car.ModelYear;
        }
    }
}
