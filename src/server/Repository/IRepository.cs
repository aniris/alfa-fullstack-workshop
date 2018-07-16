using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Server.Models;

namespace Server.Repository
{
    /// <summary>
    /// Generic repository for work with EF
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> prdicate);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        void Save();
    }
}