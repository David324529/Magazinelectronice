namespace Examples.Domain.Operations
{
    internal sealed class ValidateOrderOperation : DomainOperation<Order, object, Order>
    {
        public override Order Transform(Order order, object? state)
        {
            List<string> validationErrors = new();

            // Validarea adresei de livrare
            if (string.IsNullOrEmpty(order.DeliveryAddress))
            {
                validationErrors.Add("Adresa de livrare nu poate fi goală.");
            }

            // Validarea articolelor din comandă
            foreach (var item in order.OrderItems)
            {
                if (item.Quantity <= 0)
                {
                    validationErrors.Add($"Cantitatea pentru produsul {item.ProductId} trebuie să fie mai mare decât 0.");
                }
            }

            // Dacă există erori de validare, returnăm comanda cu erori
            if (validationErrors.Any())
            {
                order.ValidationErrors = validationErrors;
                return order;
            }

            // Dacă nu sunt erori, comanda este validă
            return order;
        }
    }
}