using Microsoft.EntityFrameworkCore;
using OrderApiApp;
using OrderApiApp.Model;
using OrderApiApp.Model.Entity;
using System.Collections;

namespace OrderApiApp.Service
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
         FmjnwaqeContext  _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository( FmjnwaqeContext  context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public TEntity FindById(long id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity?> CreateAsync(TEntity item)
        {
            
                await _dbSet.AddAsync(item);
                _context.SaveChanges();
                 return item;
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public IEnumerable<Cart> GetFullOrderInfo(int orderId) {
        return _context.Carts.Include(cart => cart.Client)
                .Include(cart => cart.Order)
                .Include(cart=>cart.Order.Product)
                .Select(cart => cart)
                .Where(cart => cart.OrderId == orderId);
         
        }
        public   Cart CreateCart(int clientId , Order order) {

             var cart =  new Cart() { ClientId = clientId, OrderId = order.Id  };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }
        public void DeleteOrder(int id) {
          var cart =  _context.Carts.First(cart => cart.OrderId == id);
            _context.Carts.Remove(cart);
           var order =  _context.Orders.First(order => order.Id == id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        
        }
        public Cheque GetCheque(int clientId) {
            var orders = _context.Carts
                .Include(cart => cart.Order)
                .Where(cart => cart.ClientId == clientId)
                .Select(cart => cart.Order);

            return new Cheque {
                Total = orders.Sum(o => o.Product.Cost),
                Products = orders.Select(o => o.Product).ToList(),
                Client = _context.Clients.Where(client=>client.Id == clientId).FirstOrDefault()
                
            };
        }
        public IEnumerable<Order>  GetAllOrders() {
            return _context.Orders.Include(o => o.Product);
        }

    }
}
 