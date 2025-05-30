﻿using Core.DataAccess;
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
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<RentalDto> GetAllDetails();
        List<RentalDto> GetDetailsByCarId(int carId);
    }
}
