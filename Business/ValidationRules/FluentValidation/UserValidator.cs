using Core.Entities.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {

        public UserValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MinimumLength(3).MaximumLength(20);

            RuleFor(c => c.LastName).NotEmpty().MinimumLength(4).MaximumLength(20);

            RuleFor(c => c.Email).NotEmpty().MinimumLength(10).MaximumLength(50).Must(email => IsAValidEmail(email));


        }

        private bool IsAValidEmail(string email)
        {
            return email.Contains('@') && (email.Contains(".com") || email.Contains(".net"));
        }
    }
}
