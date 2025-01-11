namespace Example.Data.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty; // ex: "Processing", "Shipped", "Delivered"
        public DateTime OrderDate { get; set; }
    }
}