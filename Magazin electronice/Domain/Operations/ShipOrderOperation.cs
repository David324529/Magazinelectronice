namespace Examples.Domain.Operations
{
    internal sealed class ShipOrderOperation : DomainOperation<Order, object, Order>
    {
        public override Order Transform(Order order, object? state)
        {
            if (string.IsNullOrEmpty(order.DeliveryAddress))
            {
                throw new InvalidOperationException("Adresa de livrare lipsește.");
            }

            order.Status = "Livrată";
            order.ShippingDate = DateTime.Now;

            return order;
        }
    }
}