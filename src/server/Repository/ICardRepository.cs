using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Server.Models;

namespace Server.Repository
{
    /// <summary>
    /// Generic repository for the Card class with EF
    /// </summary>
    public interface ICardRepository : IRepository<Card>
    {
        IEnumerable<Card> GetWithTransactions(Expression<Func<Card, bool>> predicate);
    }
}