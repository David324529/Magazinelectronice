using Examples.Domain.Operations;

namespace Examples.Domain.Workflows
{
    public class PlasareComandaWorkflow
    {
        private readonly ValidateOrderOperation _validateOrderOperation;
        private readonly CalculateOrderOperation _calculateOrderOperation;
        private readonly PublishOrderOperation _publishOrderOperation;

        public PlasareComandaWorkflow(
            ValidateOrderOperation validateOrderOperation,
            CalculateOrderOperation calculateOrderOperation,
            PublishOrderOperation publishOrderOperation)
        {
            _validateOrderOperation = validateOrderOperation;
            _calculateOrderOperation = calculateOrderOperation;
            _publishOrderOperation = publishOrderOperation;
        }

        public IOrderEvent Execute(ProcessOrderCommand command)
        {
            try
            {
                // Step 1: Validate the order
                var order = new Order(command.OrderItems, command.CustomerId);
                order = _validateOrderOperation.Transform(order, null);

                if (order.ValidationErrors.Any())
                {
                    return new OrderProcessFailedEvent(order.ValidationErrors); // If validation fails, return error
                }

                // Step 2: Calculate the total price
                order = _calculateOrderOperation.Transform(order, null);

                // Step 3: Publish the order (finalize the order)
                order = _publishOrderOperation.Transform(order, null);

                return new OrderProcessedEvent(order); // Return processed order
            }
            catch (Exception ex)
            {
                return new OrderProcessFailedEvent($"An error occurred during order placement: {ex.Message}");
            }
        }
    }
}