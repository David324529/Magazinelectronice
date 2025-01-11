namespace Example.Data.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string QuantityType { get; set; } = string.Empty;
    }
}