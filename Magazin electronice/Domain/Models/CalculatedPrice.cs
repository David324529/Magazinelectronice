namespace Example.Domain.Models
{
    public record CalculatedProduct(string Code, string Name, decimal Price, int Quantity, decimal TotalPrice)
    {
        public bool IsCalculated => TotalPrice == Price * Quantity;
    }
}