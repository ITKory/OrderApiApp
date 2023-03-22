using Microsoft.EntityFrameworkCore;
using OrderApiApp.Model;
using OrderApiApp.Model.Entity;

namespace OrderApiApp.Service {
    public class OrderRepository : EFGenericRepository<Order>, IOrderRepository {
        public OrderRepository(FmjnwaqeContext context) : base(context) {
        }

        public void DeleteOrder(int id) {
            var cart = _context.Carts.First(cart => cart.OrderId == id);
            _context.Carts.Remove(cart);
            var order = _context.Orders.First(order => order.Id == id);
            _context.Orders.Remove(order);
            _context.SaveChanges();

        }
        public IEnumerable<Order> GetAllOrders() {
            return _context.Orders.Include(o => o.Product);
        }
    }
}
