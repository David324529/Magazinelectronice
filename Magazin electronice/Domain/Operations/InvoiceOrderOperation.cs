namespace Examples.Domain.Operations
{
    internal sealed class InvoiceOrderOperation : DomainOperation<Order, object, Order>
    {
        public override Order Transform(Order order, object? state)
        {
            if (order.TotalPrice <= 0)
            {
                throw new InvalidOperationException("Nu se poate emite factura pentru o comandă cu preț total 0.");
            }

            // Generăm un număr de factură (pentru exemplu, se poate folosi un GUID sau o secvență)
            order.InvoiceNumber = Guid.NewGuid().ToString();
            order.Status = "Facturată"; // Actualizăm statusul comenzii
            order.InvoiceDate = DateTime.Now; // Data emiterii facturii

            return order; // Returnăm comanda cu factura generată
        }
    }
}