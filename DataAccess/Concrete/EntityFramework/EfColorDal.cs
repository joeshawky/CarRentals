﻿using Core.DataAccess;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EntityRepositoryBase<Color, KodlamaioContext>, IColorDal
    {
    }
}
