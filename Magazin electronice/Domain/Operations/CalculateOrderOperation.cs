namespace Examples.Domain.Operations
{
    internal sealed class CalculateOrderOperation : DomainOperation<Order, object, Order>
    {
        public override Order Transform(Order order, object? state)
        {
            // Calculul prețului total
            decimal totalPrice = order.OrderItems.Sum(item => item.Price * item.Quantity);
            order.TotalPrice = totalPrice;

            return order;
        }
    }
}