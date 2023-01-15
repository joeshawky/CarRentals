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
using DataAccess.Migrations;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }


        public IResult AddCarImage(int carId, IFormFile imageFile)
        {
            var result = Validator.Run(
                CarExists(carId),
                CarHasLessThanFiveImages(carId),
                ImageFileIsSupported(imageFile)
                );


            if (result.Success == false)
                return result;


            var processedImageResult = ProcessImage(imageFile);

            if (processedImageResult.Success is false)
                return processedImageResult;


            string imageFullPath = (string) processedImageResult.Data;


            var carImage = new CarImage
            {
                CarId = carId,
                ImagePath = imageFullPath
            };

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.CarImageAdded);
        }



        [ValidationAspect(typeof(CarImageValidator))]
        public IResult UpdateCarImage(CarImage newCarImage, IFormFile imageFile)
        {
            var result = Validator.Run(
                CarExists(newCarImage.CarId),
                VerifyCarImageId(newCarImage.Id),
                ImageFileIsSupported(imageFile)
                );

            if (result.Success == false)
                return result;

            var carImage = _carImageDal.Get(c => c.Id == newCarImage.Id);
            
            var imgPath = carImage.ImagePath;

            if (DeleteImage(imgPath) == false)
                return new ErrorResult(Messages.ImageNotFound);

            var createNewImageResult = CreateImage(imgPath, imageFile);

            if (createNewImageResult.Success is false)
                return new ErrorResult(Messages.CarImageCreationError);


            return new SuccessResult(Messages.CarImageUpdated);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult RemoveCarImage(CarImage carImage)
        {
            var result = Validator.Run(
                CarExists(carImage.CarId),
                VerifyCarImageValues(carImage)
                );

            if (result.Success is false)
                return result;


            if (DeleteImage(carImage.ImagePath) == false)
                return new ErrorResult(Messages.ImageNotFound);

            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }


        private IResult CheckImagePath(string imagePath)
        {
            if (File.Exists(imagePath))
                return new SuccessResult();

            return new ErrorResult(Messages.ImageNotFound);
        }

        private IResult CarExists(int carId)
        {
            var car = _carService.GetById(carId);
            if (car.Success)
                return new SuccessResult();

            return new ErrorResult(Messages.CarNotFound);

        }

        private IResult CarHasLessThanFiveImages(int carId)
        {
            var car = _carImageDal.GetAll(c => c.CarId == carId);

            if (car.Count < 5)
                return new SuccessResult();

            return new ErrorResult(Messages.MaxImagesPerCarWasReached);
        }

        private string CreateImageName()
        {
            return Guid
                .NewGuid()
                .ToString("N")
                .Substring(0, 15);
        }

        private IResult CreateImage(string fullPath, IFormFile imageFile)
        {

            try
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                return new SuccessResult(Messages.imagecreated);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }

        }

        private IResult ImageFileIsSupported(IFormFile imageFile)
        {
            var supportedTypes = FileTypes.Types;
            foreach (var item in supportedTypes)
            {
                if (item.Key == imageFile.ContentType)
                    return new SuccessResult(Messages.SupportedImageType);
            }

            return new ErrorResult(Messages.UnsupportedImageType);
        }
    


        private IDataResult<object> ProcessImage(IFormFile imageFile)
        {
            var imageName = CreateImageName();

            var imageType = GetImageType(imageFile);

            var imageFullPath = $"{Paths.ImagesPath}{imageName}.{imageType}";

            var createImageResult = CreateImage(imageFullPath, imageFile);

            if (createImageResult.Success)
                return new SuccessDataResult<object>(imageFullPath, Messages.ImageProcessed);

            return new ErrorDataResult<object>(Messages.ImageProcessError);

        }
        
        private string GetImageType(IFormFile imageFile)
        {
            foreach (var item in FileTypes.Types)
            {
                if (item.Key == imageFile.ContentType)
                    return item.Value;
            }
            return string.Empty;
        }

        private bool DeleteImage(string imagePath)
        {
            if (File.Exists(imagePath) is false)
                return false;

            File.Delete(imagePath);
            return true;
        }


        private IResult VerifyCarImageId(int carImageId)
        {
            var carImageSrc = _carImageDal.GetAll(ci => ci.CarId == carImageId).Any();

            if (carImageSrc)
                return new SuccessResult();
            
            
            return new ErrorResult(Messages.InaccurateCarImage);
        }
        private IResult VerifyCarImageValues(CarImage carImage)
        {
            if (CarExists(carImage.Id).Success == false)
            {
                return new ErrorResult(Messages.CarImageNotFound);
            }
            var carImageSrc = _carImageDal.Get(c => c.Id == carImage.Id);

            if (carImageSrc == null)
                return new ErrorResult(Messages.CarImageNotFound);


            if (ObjectsComparer.CompareByValues(carImageSrc, carImage) == false)
                return new ErrorResult(Messages.InaccurateCarImage);
            

            return new SuccessResult();
        }

    }
}
