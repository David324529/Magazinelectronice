namespace Examples.Domain.Operations
{
    internal sealed class PublishOrderOperation : DomainOperation<Order, object, Order>
    {
        public override Order Transform(Order order, object? state)
        {
            if (order.TotalPrice <= 0)
            {
                throw new InvalidOperationException("Prețul total al comenzii trebuie să fie mai mare decât 0.");
            }

            // Marcare comanda ca finalizată
            order.Status = "Publicată";
            order.PublishedDate = DateTime.Now;

            return order;
        }
    }
}