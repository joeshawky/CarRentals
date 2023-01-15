using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;

        public CarManager(ICarDal carDal, 
        IBrandService brandService, 
        IColorService colorService)
        {
            _carDal = carDal;
            _brandService = brandService;
            _colorService = colorService;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            var result = Validator.Run(
                BrandExists(car.BrandId),
                ColorExists(car.ColorId));

            if (result.Success == false)
                return result;

            _carDal.Add(car);

            return new SuccessResult(Messages.CarAdded);

        }

        public IDataResult<List<Car>> GetAll(Func<Car, bool>? filter = null)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(filter));
        }

        public IDataResult<Car> GetById(int id)
        {
            var car = _carDal.Get(c => c.Id == id);

            if (car is null)
                return new ErrorDataResult<Car>(Messages.CarNotFound);

            return new SuccessDataResult<Car>(car);
        }

        public IResult DeleteById(int carId)
        {
            var result = GetById(carId);

            if (result.Success is false)
                return new ErrorResult(Messages.CarNotFound);

            _carDal.Delete(result.Data);

            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            var result = Validator.Run(BrandExists(brandId));

            if (result.Success == false)
                return new ErrorDataResult<List<Car>>(Messages.BrandNotFound);

            var cars = _carDal.GetAll(c => c.BrandId == brandId);

            return new SuccessDataResult<List<Car>>(cars);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            var result = Validator.Run(ColorExists(colorId));

            if (result.Success == false)
                return new ErrorDataResult<List<Car>>(Messages.ColorNotFound);

            var cars = _carDal.GetAll(c => c.ColorId == colorId);

            return new SuccessDataResult<List<Car>>(cars);

        }

        public IDataResult<List<CarDto>> GetCarsDetails()
        {
            var cars = _carDal.GetCarsDetails();
            cars.ForEach(c =>
            {
                if (c.ImagesUrls.Count == 0)
                {
                    c.ImagesUrls = new List<string>() {CarInfo.DefaultImage };
                }
            });
            return new SuccessDataResult<List<CarDto>>(cars);
        }

        public IDataResult<CarDto> GetCarDetailsByCarId(int carId)
        {
            var car = _carDal.GetCarDetailsByCarId(carId);
           
            if (car.ImagesUrls.Count == 0)
            {
                car.ImagesUrls = new List<string>() { CarInfo.DefaultImage };
            }
          

            return new SuccessDataResult<CarDto>(car);
        }

        public IDataResult<List<CarDto>> GetCarsDetailsByBrandName(string brandName)
        {
            var car = _carDal.GetCarsDetailsByBrandName(brandName);

            car.ForEach(c =>
            {
                if (c.ImagesUrls.Count == 0)
                {
                    c.ImagesUrls = new List<string>() { CarInfo.DefaultImage };
                }
            });

            return new SuccessDataResult<List<CarDto>>(car);
        }

        public IDataResult<List<CarDto>> GetCarsDetailsByBrandId(int brandId)
        {
            var car = _carDal.GetCarsDetailsByBrandId(brandId);

            car.ForEach(c =>
            {
                if (c.ImagesUrls.Count == 0)
                {
                    c.ImagesUrls = new List<string>() { CarInfo.DefaultImage };
                }
            });

            return new SuccessDataResult<List<CarDto>>(car);
        }

        public IDataResult<List<CarDto>> GetCarsDetailsByColorId(int colorId)
        {
            var car = _carDal.GetCarsDetailsByColorId(colorId);
            car.ForEach(c =>
            {
                if (c.ImagesUrls.Count == 0)
                {
                    c.ImagesUrls = new List<string>() { CarInfo.DefaultImage };
                }
            });

            return new SuccessDataResult<List<CarDto>>(car);
        }

        public IResult Update(Car car)
        {
            var result = Validator.Run(VerifyById(car.Id));
            if (result.Success == false)
            {
                return new ErrorResult(Messages.InaccurateCar);
            }

            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);

        }

        public IResult Delete(Car car)
        {
            var result = Validator.Run(VerifyCarValues(car));
            if (result.Success == false)
            {
                return new ErrorResult(Messages.InaccurateCar);
            }

            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        private IResult VerifyById(int carId)
        {
            var result = _carDal.GetAll(c => c.Id == carId).Any();

            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarNotFound);
        }

        private IResult VerifyCarValues(Car car)
        {
            var carSrc = _carDal.Get(c => c.Id == car.Id);
            if (carSrc == null)
                return new ErrorResult(Messages.CarNotFound);


            var result = ObjectsComparer.CompareByValues(car, carSrc);
            if (result)
                return new SuccessResult();

            return new ErrorResult(Messages.InaccurateCar);
        }

        private IResult BrandExists(int brandId)
        {
            var result = _brandService.GetAll(b => b.Id == brandId);

            if (result.Data.Count >0)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.BrandNotFound);
        }

        private IResult ColorExists(int colorId)
        {
            var result = _colorService.GetAll(c => c.Id == colorId);

            if (result.Data.Count > 0)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.ColorNotFound);
        }


    }


}
