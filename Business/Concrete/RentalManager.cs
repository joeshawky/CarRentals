using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Resource;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly ICustomerDal _customerDal;
        private readonly ICarDal _carDal;

        public RentalManager(IRentalDal rentalDal, ICustomerDal customerDal, ICarDal carDal)
        {
            _rentalDal = rentalDal;
            _customerDal = customerDal;
            _carDal = carDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult AddRental(Rental rental)
        {

            var result = Validator.Run(
                IsAValidCar(rental.CarId),
                IsAValidCustomer(rental.CustomerId),
                IsCarAvailable(rental.CarId, rental.RentDate, rental.ReturnDate)
                );

            if (result.Success == false)
                return result;

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Update(Rental rental)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int rentalId)
        {
            var result = Validator.Run(VerifyRentalExists(rentalId));
            if (result.Success == false)
            {
                return new ErrorResult(Messages.RentalNotFound);
            }
            var rental = _rentalDal.Get(r => r.Id == rentalId);
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }



        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<RentalDto>> GetAllDetails()
        {
            var rentals = _rentalDal.GetAllDetails();
            return new SuccessDataResult<List<RentalDto>>(rentals);
        }
        public IDataResult<List<RentalDto>> GetDetailsByCarId(int carId)
        {
            var rental = _rentalDal.GetDetailsByCarId(carId);
            if (rental == null)
            {
                return new ErrorDataResult<List<RentalDto>>(Messages.RentalNotFound);
            }

            return new SuccessDataResult<List<RentalDto>>(rental);
        }


        public IResult VerifyRentalExists(int rentalId)
        {
            var rentalInDb = _rentalDal.GetAll(r => r.Id == rentalId);
            if (rentalInDb.Any())
                return new SuccessResult();

            return new ErrorResult();
        }


        private IResult IsCarAvailable(int carId, DateTime reservationDate, DateTime? returnDate)
        {
            var carRentals = _rentalDal.GetAll(r => r.CarId == carId);


            // Check if there is any rental where the reservation date falls within the rental date range
            bool reservationCheck = carRentals.Any(r => reservationDate >= r.RentDate && reservationDate <= r.ReturnDate);
            if (reservationCheck)
            {
                return new ErrorResult(Messages.CarIsRented);
            }

            // Check if there is any rental where the return date is not null and falls within the rental date range
            if (returnDate != null)
            {
                bool returnCheck = carRentals.Any(r => returnDate >= r.RentDate && returnDate <= r.ReturnDate);

                if (returnCheck)
                {
                    return new ErrorResult(Messages.CarIsRented);
                }
            }
            // Check if the car is already rented without a return date
            bool rentedWithoutReturnDate = carRentals.Any(r => r.ReturnDate == null && r.RentDate <= reservationDate);
            if (rentedWithoutReturnDate)
            {
                return new ErrorResult(Messages.CarIsRentedWithoutReturnDate);
            }

            // If the code reaches this point, the car is available
            return new SuccessResult();


        }
        private IResult IsAValidCustomer(int customerId)
        {
            var result = _customerDal.GetAll(c => c.Id == customerId).Any();
            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.InvalidCustomer);
        }

        private IResult IsAValidCar(int carId)
        {
            var result = _carDal.GetAll(c => c.Id == carId).Any();
            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.InaccurateCar);
        }


    }
}
