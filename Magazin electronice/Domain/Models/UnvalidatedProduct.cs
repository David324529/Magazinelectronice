namespace Example.Domain.Models
{
    public record UnvalidatedProduct(string Code, string Name, decimal? Price, int? Quantity);
}