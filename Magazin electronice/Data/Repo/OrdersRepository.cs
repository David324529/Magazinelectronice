using Example.Data.Models;

namespace Example.Data.Repositories
{
    public class OrdersRepository
    {
        private readonly ShoppingContext _context;

        public OrdersRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task SaveOrderAsync(OrderDto order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}