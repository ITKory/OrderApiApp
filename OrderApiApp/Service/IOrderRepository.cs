using OrderApiApp.Model.Entity;

namespace OrderApiApp.Service;
    public interface IOrderRepository:IGenericRepository<Order> {
    public void DeleteOrder(int id);
    public IEnumerable<Order> GetAllOrders();
}

