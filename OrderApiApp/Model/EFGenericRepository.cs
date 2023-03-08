using Microsoft.EntityFrameworkCore;
using OrderApiApp.Model.Entity;

namespace OrderApiApp.Model
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        YguckjysContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(YguckjysContext context)
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

    }
}
