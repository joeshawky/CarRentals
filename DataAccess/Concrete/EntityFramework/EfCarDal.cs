using Core.DataAccess;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EntityRepositoryBase<Car, KodlamaioContext>, ICarDal
    {
        public List<CarDto> GetCarsDetails()
        {
            using (var context = new KodlamaioContext())
            {
                var images = context.CarImages.ToList();

                var cars = (from car in context.Cars
                           join brand in context.Brands
                           on car.BrandId equals brand.Id
                           join color in context.Colors
                           on car.ColorId equals color.Id
                           select new CarDto
                           {
                               CarId = car.Id,
                               CarName = car.Description,
                               BrandName = brand.Name,
                               ColorName = color.Name,
                               DailyPrice = car.DailyPrice,

                           }).ToList();

                cars.ForEach(c =>
                {
                    c.ImagesUrls = context.CarImages
                    .Where(i => i.CarId == c.CarId)
                    .Select(i => i.ImagePath)
                    .ToList();
                });

                return cars;

            }
        }

        public CarDto GetCarDetailsByCarId(int carId)
        {
            using (var context = new KodlamaioContext())
            {
                var cars = (from car in context.Cars
                            join brand in context.Brands
                            on car.BrandId equals brand.Id
                            join color in context.Colors
                            on car.ColorId equals color.Id
                            where car.Id == carId
                            select new CarDto
                            {
                                CarId= car.Id,
                                CarName = car.Description,
                                BrandName = brand.Name,
                                ColorName = color.Name,
                                DailyPrice = car.DailyPrice
                            }).ToList();

                cars.ForEach(c =>
                {
                    c.ImagesUrls = context.CarImages
                    .Where(i => i.CarId == c.CarId)
                    .Select(i => i.ImagePath)
                    .ToList();
                });

                return cars[0];

            }
        }

        public List<CarDto> GetCarsDetailsByBrandId(int brandId)
        {
            using (var context = new KodlamaioContext())
            {
                var cars = (from car in context.Cars
                           join brand in context.Brands
                           on car.BrandId equals brand.Id
                           join color in context.Colors
                           on car.ColorId equals color.Id
                           where car.BrandId == brandId
                           select new CarDto
                           {
                               CarId = car.Id,
                               CarName = car.Description,
                               BrandName = brand.Name,
                               ColorName = color.Name,
                               DailyPrice = car.DailyPrice
                           }).ToList();

                cars.ForEach(c =>
                {
                    c.ImagesUrls = context.CarImages
                    .Where(i => i.CarId == c.CarId)
                    .Select(i => i.ImagePath)
                    .ToList();
                });

                return cars;

            }
        }

        public List<CarDto> GetCarsDetailsByColorId(int colorId)
        {
            using (var context = new KodlamaioContext())
            {
                var cars = (from car in context.Cars
                            join brand in context.Brands
                            on car.BrandId equals brand.Id
                            join color in context.Colors
                            on car.ColorId equals color.Id
                            where car.ColorId == colorId
                            select new CarDto
                            {
                                CarId = car.Id,
                                CarName = car.Description,
                                BrandName = brand.Name,
                                ColorName = color.Name,
                                DailyPrice = car.DailyPrice
                            }).ToList();

                cars.ForEach(c =>
                {
                    c.ImagesUrls = context.CarImages
                    .Where(i => i.CarId == c.CarId)
                    .Select(i => i.ImagePath)
                    .ToList();
                });

                return cars;

            }
        }

        public List<CarDto> GetCarsDetailsByBrandName(string brandName)
        {
            using (var context = new KodlamaioContext())
            {
                var cars = (from car in context.Cars
                            join brand in context.Brands
                            on car.BrandId equals brand.Id
                            join color in context.Colors
                            on car.ColorId equals color.Id
                            where brand.Name == brandName
                            select new CarDto
                            {
                                CarId = car.Id,
                                CarName = car.Description,
                                BrandName = brand.Name,
                                ColorName = color.Name,
                                DailyPrice = car.DailyPrice
                            }).ToList();

                cars.ForEach(c =>
                {
                    c.ImagesUrls = context.CarImages
                    .Where(i => i.CarId == c.CarId)
                    .Select(i => i.ImagePath)
                    .ToList();
                });

                return cars;

            }
        }
    }
}
