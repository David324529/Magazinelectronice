namespace Examples.Domain.Workflows
{
    public class FacturareWorkflow
    {
        private readonly InvoiceOrderOperation _invoiceOrderOperation;

        public FacturareWorkflow(InvoiceOrderOperation invoiceOrderOperation)
        {
            _invoiceOrderOperation = invoiceOrderOperation;
        }

        public IOrderEvent Execute(Order order)
        {
            try
            {
                // Step: Invoice the order
                order = _invoiceOrderOperation.Transform(order, null);

                return new OrderProcessedEvent(order); // Return the invoiced order
            }
            catch (Exception ex)
            {
                return new OrderProcessFailedEvent($"An error occurred during invoicing: {ex.Message}");
            }
        }
    }
}