using FluentValidation;
using FluentValidation_Demo.Dto;

namespace FluentValidation_Demo.Validators

{

    using FluentValidation;
    using FluentValidation_Demo.Resources;
    using Microsoft.Extensions.Localization;

    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator(IStringLocalizer<SharedResource> localizer)
        {
            var value = localizer["ProductNameRequired"].Value;

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product Id is required.");

            // Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer["ProductNameRequired"])
                .Length(3, 100)
                .WithMessage("Product name must be between 3 and 100 characters.");

            // Description
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            // Price
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.")
                .LessThan(100000)
                .WithMessage("Price cannot exceed 100,000.");

            // Stock
            RuleFor(x => x.Stock)
                .InclusiveBetween(0, 10000)
                .WithMessage("Stock must be between 0 and 10,000.");

            // Weight
            RuleFor(x => x.Weight)
                .GreaterThan(0)
                .WithMessage("Weight must be greater than 0.");

            // Manufacture Date
            RuleFor(x => x.ManufactureDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Manufacture date cannot be in the future.");

            // Expiry Date
            RuleFor(x => x.ExpiryDate)
                .GreaterThan(x => x.ManufactureDate)
                .When(x => x.ExpiryDate.HasValue)
                .WithMessage("Expiry date must be after the manufacture date.");

            // SKU
            RuleFor(x => x.SKU)
                .NotEmpty()
                .WithMessage("SKU is required.")
                .Matches(@"^[A-Z]{3}-\d{5}$")
                .WithMessage("SKU must follow the format ABC-12345.");

            // Category
            RuleFor(x => x.Category)
                .Must(category => new[]
                {
                "Electronics",
                "Books",
                "Food",
                "Furniture"
                }.Contains(category))
                .WithMessage("Category must be Electronics, Books, Food, or Furniture.");

            // Website
            RuleFor(x => x.Website)
                .Must(uri => uri == null || uri.IsAbsoluteUri)
                .WithMessage("Website must be a valid absolute URL.");

            // Tags
            RuleFor(x => x.Tags)
                .NotEmpty()
                .WithMessage("At least one tag is required.")
                .Must(tags => tags.Count <= 10)
                .WithMessage("A maximum of 10 tags is allowed.");

            RuleForEach(x => x.Tags)
                .NotEmpty()
                .WithMessage("Tag cannot be empty.")
                .MaximumLength(20)
                .WithMessage("Each tag cannot exceed 20 characters.");

            // Nested Object
            RuleFor(x => x.Dimensions)
                .NotNull()
                .WithMessage("Dimensions are required.")
                .SetValidator(new DimensionsValidator());

            // Collection
            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("At least one product image is required.");

            RuleForEach(x => x.Images)
                .SetValidator(new ProductImageValidator());

            // Conditional Rule
            When(x => x.IsAvailable, () =>
            {
                RuleFor(x => x.Stock)
                    .GreaterThan(0)
                    .WithMessage("Stock must be greater than 0 when the product is available.");
            });
        }
    }
}
