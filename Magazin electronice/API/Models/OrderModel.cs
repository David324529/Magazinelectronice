using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Api.Models
{
    public class OrderModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required]
        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
    }

    public class OrderItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}