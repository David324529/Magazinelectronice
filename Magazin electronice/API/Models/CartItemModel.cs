using System.ComponentModel.DataAnnotations;

namespace Example.Api.Models
{
    public class CartItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}