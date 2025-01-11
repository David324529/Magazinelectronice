namespace Examples.Domain.Operations
{
    internal sealed class OrderProcessingOperation : DomainOperation<Order, object, Order>
    {
        private readonly ValidateOrderOperation _validateOrderOperation;
        private readonly CalculateOrderOperation _calculateOrderOperation;
        private readonly PublishOrderOperation _publishOrderOperation;

        public OrderProcessingOperation(
            ValidateOrderOperation validateOrderOperation,
            CalculateOrderOperation calculateOrderOperation,
            PublishOrderOperation publishOrderOperation)
        {
            _validateOrderOperation = validateOrderOperation;
            _calculateOrderOperation = calculateOrderOperation;
            _publishOrderOperation = publishOrderOperation;
        }

        public override Order Transform(Order order, object? state)
        {
            // Validăm comanda
            order = _validateOrderOperation.Transform(order, state);

            if (order.ValidationErrors.Any())
            {
                return order; // Dacă sunt erori de validare, returnăm comanda invalidă
            }

            // Calculăm prețul total
            order = _calculateOrderOperation.Transform(order, state);

            // Publicăm comanda
            order = _publishOrderOperation.Transform(order, state);

            return order; // Comanda este complet procesată
        }
    }
}