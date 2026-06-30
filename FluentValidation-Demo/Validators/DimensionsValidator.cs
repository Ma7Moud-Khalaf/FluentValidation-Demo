using FluentValidation;
using FluentValidation_Demo.Dto;

namespace FluentValidation_Demo.Validators
{
    public class DimensionsValidator : AbstractValidator<DimensionsDto>
    {
        public DimensionsValidator()
        {
            RuleFor(x => x.Length).GreaterThan(0);
            RuleFor(x => x.Width).GreaterThan(0);
            RuleFor(x => x.Height).GreaterThan(0);
        }
    }
}
