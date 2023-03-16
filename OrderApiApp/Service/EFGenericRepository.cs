﻿using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Cart> GetFullOrderInfo(int id) {
          var a =   _context.Carts.Select(o=>o).Where(o=>o.OrderId == id).ToList();
            return a;
        }

    }
}
