using FluentValidation;
using FluentValidation_Demo.Dto;

namespace FluentValidation_Demo.Validators
{
    public class ProductImageValidator : AbstractValidator<ProductImageDto>
    {
        public ProductImageValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Invalid image URL.");
        }
    }
}
