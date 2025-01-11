using Microsoft.EntityFrameworkCore;

namespace Example.Data
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
        }

        public DbSet<ProductDto> Products { get; set; }
        public DbSet<CustomerDto> Customers { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<OrderItemDto> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDto>().ToTable("Product")
                .HasKey(p => p.ProductId);
            
            modelBuilder.Entity<CustomerDto>().ToTable("Customer")
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<OrderDto>().ToTable("Order")
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<OrderItemDto>().ToTable("OrderItem")
                .HasKey(oi => oi.OrderItemId);
        }
    }
}