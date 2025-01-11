namespace Example.Domain.Models
{
    public record ValidatedProduct(string Code, string Name, decimal Price, int Quantity)
    {
        public bool IsValid => Price > 0 && Quantity > 0;
    }
}