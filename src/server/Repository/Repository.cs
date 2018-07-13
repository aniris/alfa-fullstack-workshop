using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Server.Core;
using Server.Models;

namespace Server.Repository
{
    /// <summary>
    /// Base reasisation Repository for EF
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SQLContext _context;
        protected DbSet<TEntity> _collection;
        public Repository(SQLContext context)
        {
            _context = context;
            _collection = context.Set<TEntity>();
        }
        public void Add(TEntity entity) => _collection.Add(entity);

        public void Delete(int id)
        {
            TEntity entityToDelete = _collection.Find(id);

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _collection.Attach(entityToDelete);

            _collection.Remove(entityToDelete);
        }

        public TEntity Get(int id) => _collection.Find(id);

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => _collection.Where(predicate).ToList();

        public IEnumerable<TEntity> GetAll() => _collection.ToList();

        public void Save() => _context.SaveChanges();

        public void Update(TEntity entity)
        {
            _collection.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}