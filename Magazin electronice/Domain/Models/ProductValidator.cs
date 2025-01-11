namespace Example.Domain.Models
{
    public static class ProductValidator
    {
        public static ValidatedProduct ValidateProduct(UnvalidatedProduct unvalidatedProduct)
        {
            if (unvalidatedProduct.Price.HasValue && unvalidatedProduct.Quantity.HasValue)
            {
                return new ValidatedProduct(unvalidatedProduct.Code, unvalidatedProduct.Name, unvalidatedProduct.Price.Value, unvalidatedProduct.Quantity.Value);
            }

            throw new InvalidOperationException("Produsul este invalid: Preț sau cantitate lipsesc.");
        }

        public static CalculatedProduct CalculateProduct(ValidatedProduct validatedProduct)
        {
            decimal totalPrice = validatedProduct.Price * validatedProduct.Quantity;
            return new CalculatedProduct(validatedProduct.Code, validatedProduct.Name, validatedProduct.Price, validatedProduct.Quantity, totalPrice);
        }
    }
}