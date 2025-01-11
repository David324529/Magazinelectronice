using System.ComponentModel.DataAnnotations;

namespace Example.Api.Models
{
    public class ProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string QuantityType { get; set; } = string.Empty;
    }
}