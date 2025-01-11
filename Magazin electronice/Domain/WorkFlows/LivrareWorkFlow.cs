namespace Examples.Domain.Workflows
{
    public class LivrareWorkflow
    {
        private readonly ShipOrderOperation _shipOrderOperation;

        public LivrareWorkflow(ShipOrderOperation shipOrderOperation)
        {
            _shipOrderOperation = shipOrderOperation;
        }

        public IOrderEvent Execute(Order order)
        {
            try
            {
                // Step: Ship the order (mark as delivered)
                order = _shipOrderOperation.Transform(order, null);

                return new OrderProcessedEvent(order); // Return the shipped order
            }
            catch (Exception ex)
            {
                return new OrderProcessFailedEvent($"An error occurred during shipping: {ex.Message}");
            }
        }
    }
}