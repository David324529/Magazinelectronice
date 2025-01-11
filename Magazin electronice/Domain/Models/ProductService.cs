using Example.Domain.Models;

public class ProductService
{
    public CalculatedProduct ProcessProduct(UnvalidatedProduct unvalidatedProduct)
    {
        // Validăm produsul
        var validatedProduct = ProductValidator.ValidateProduct(unvalidatedProduct);
        
        // Calculăm prețul total
        var calculatedProduct = ProductValidator.CalculateProduct(validatedProduct);
        
        return calculatedProduct;
    }
}