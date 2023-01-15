﻿using Core.DataAccess;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImage : EntityRepositoryBase<CarImage, KodlamaioContext>, ICarImageDal
    {
    }
}
