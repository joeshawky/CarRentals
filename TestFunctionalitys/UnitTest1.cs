using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;

namespace TestFunctionalitys
{

    public class IsCarAvailableFunctionTest
    {
        private readonly List<Rental> CarRental = new List<Rental>()
        {
            new Rental() {CarId= 0, CustomerId=1, Id=1, RentDate=new DateTime(2023, 1, 14), ReturnDate=new DateTime(2023, 1, 20)},
            new Rental() {CarId= 0, CustomerId=1, Id=1, RentDate=new DateTime(2023, 1, 25)}
        };

        private IResult IsCarAvailable(int carId, DateTime reservationDate, DateTime? returnDate)
        {
            var carRentals = CarRental;


            bool reservationCheck = carRentals.Any(r => reservationDate >= r.RentDate && reservationDate <= r.ReturnDate);
            if (reservationCheck)
            {
                return new ErrorResult();
            }

            // Check if there is any rental where the return date is not null and falls within the rental date range
            if (returnDate != null)
            {
                bool returnCheck = carRentals.Any(r => returnDate >= r.RentDate && returnDate <= r.ReturnDate);
                
                if (returnCheck)
                {
                    return new ErrorResult();
                }
            }
            // Check if the car is already rented without a return date
            bool rentedWithoutReturnDate = carRentals.Any(r => r.ReturnDate == null && r.RentDate < reservationDate);
            if (rentedWithoutReturnDate)
            {
                return new ErrorResult();
            }

            // If the code reaches this point, the car is available
            return new SuccessResult();



        }

      

        [Fact]
        public void AddRentalWithReturn_Success_1()
        {
            var reservationDate = new DateTime(2023, 1, 12);
            var returnDate = new DateTime(2023, 1, 13);

            Assert.True(IsCarAvailable(0, reservationDate, returnDate).Success);
        }

        [Fact]
        public void AddRentalWithoutReturn_Success_1()
        {
            var reservationDate = new DateTime(2023, 1, 12);

            Assert.True(IsCarAvailable(0, reservationDate, null).Success);
        }

        [Fact]
        public void AddRentalWithReturn_Error_1()
        {
            var reservationDate = new DateTime(2023, 1, 10);
            var returnDate = new DateTime(2023, 1, 15);

            Assert.False(IsCarAvailable(0, reservationDate, returnDate).Success);
        }

        [Fact]
        public void AddRentalWithReturn_Error_2()
        {
            var reservationDate = new DateTime(2023, 1, 15);
            var returnDate = new DateTime(2023, 1, 18);

            Assert.False(IsCarAvailable(0, reservationDate, returnDate).Success);
        }

        [Fact]
        public void AddRentalWithReturn_Error_3()
        {
            var reservationDate = new DateTime(2023, 1, 16);
            var returnDate = new DateTime(2023, 1, 23);

            Assert.False(IsCarAvailable(0, reservationDate, returnDate).Success);
        }

        [Fact]
        public void AddRentalWithoutReturn_Error_4()
        {
            var reservationDate = new DateTime(2023, 1, 26);

            Assert.False(IsCarAvailable(0, reservationDate, null).Success);
        }

    }
}