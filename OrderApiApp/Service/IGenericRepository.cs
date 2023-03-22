using OrderApiApp.Model.Entity;

namespace OrderApiApp.Service
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> CreateAsync(TEntity item);
        TEntity FindById(long id);
        void Remove(TEntity item);
        void Update(TEntity item);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        IEnumerable<Cart> GetFullOrderInfo(int id);
        Cheque GetCheque(int clientId);
        public   Cart CreateCart(int clientId, Order order);

    }
}
