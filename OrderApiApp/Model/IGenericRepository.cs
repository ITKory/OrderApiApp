namespace OrderApiApp.Model
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> CreateAsync(TEntity item);
        TEntity FindById(long id);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
