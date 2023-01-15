using Core.DataAccess;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EntityRepositoryBase<Rental, KodlamaioContext>, IRentalDal
    {
        public List<RentalDto> GetAllDetails()
        {
            using (var context = new KodlamaioContext())
            {
                var rentals = from rental in context.Rentals
                              join customer in context.Customers
                              on rental.CustomerId equals customer.Id
                              join user in context.Users
                              on customer.UserId equals user.Id
                              join car in context.Cars
                              on rental.CarId equals car.Id
                              join brand in context.Brands
                              on car.BrandId equals brand.Id
                              select new RentalDto
                              {
                                  BrandName = brand.Name,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  RentDate = rental.RentDate,
                                  ReturnDate = rental.ReturnDate
                              };

                return rentals.ToList();
            }
        }

        public List<RentalDto> GetDetailsByCarId(int carId)
        {
            using (var context = new KodlamaioContext())
            {
                var rentals = from rental in context.Rentals
                              join customer in context.Customers
                              on rental.CustomerId equals customer.Id
                              join user in context.Users
                              on customer.UserId equals user.Id
                              join car in context.Cars
                              on rental.CarId equals car.Id
                              where car.Id == carId
                              join brand in context.Brands
                              on car.BrandId equals brand.Id
                              select new RentalDto
                              {
                                  BrandName = brand.Name,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  RentDate = rental.RentDate,
                                  ReturnDate = rental.ReturnDate
                              };

                return rentals.ToList();
            }
        }
    }
}
