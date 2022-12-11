using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RentValidator : AbstractValidator<Rent>
    {
        public RentValidator()
        {
            RuleFor(r => r.RentDate).NotEmpty();
            RuleFor(r => r.ReturnDate).NotEmpty();
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.CustomerId).NotEmpty();
            RuleFor(r => r.RentDate).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(r => r.ReturnDate).GreaterThanOrEqualTo(DateTime.Now);

        }
    }
}


