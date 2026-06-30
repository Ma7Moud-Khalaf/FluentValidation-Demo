namespace FluentValidation_Demo.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public double Weight { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool IsAvailable { get; set; }

        public string SKU { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public Uri? Website { get; set; }

        public List<string> Tags { get; set; } = [];

        public List<ProductImageDto> Images { get; set; } = [];

        public DimensionsDto Dimensions { get; set; } = new();
    }
}
