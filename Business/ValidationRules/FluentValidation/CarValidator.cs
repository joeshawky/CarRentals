﻿using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Description).NotEmpty().MinimumLength(2);

            RuleFor(c => c.DailyPrice).GreaterThan(0);

            RuleFor(c => c.BrandId).NotEmpty();

            RuleFor(c => c.ColorId).NotEmpty();

            RuleFor(c => c.ModelYear)
                .LessThanOrEqualTo(CarInfo.MaxModelYear)
                .GreaterThanOrEqualTo(CarInfo.MinModelYear);



        }
    }
}
