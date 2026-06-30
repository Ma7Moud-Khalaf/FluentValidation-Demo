namespace FluentValidation_Demo.Dto
{
    public class ProductImageDto
    {
        public string Url { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }
    }
}
