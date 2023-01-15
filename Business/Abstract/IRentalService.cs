using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult AddRental(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(int rentalId);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalDto>> GetAllDetails();
        IDataResult<List<RentalDto>> GetDetailsByCarId(int carId);


    }
}
