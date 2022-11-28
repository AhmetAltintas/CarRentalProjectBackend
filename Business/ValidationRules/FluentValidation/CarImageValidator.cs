using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(cı => cı.CarId).NotEmpty();
            RuleFor(cı => cı.ImagePath).NotEmpty();
        }
    }
}






