using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Server.Core;
using Server.Models;

namespace Server.Repository
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly SQLContext _context;

        public TransactionRepository(SQLContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Transaction> GetAll()
        {
            return (IEnumerable<Transaction>)_context.Transactions;
        }

        public Transaction GetById(int id)
        {
            return _context.Transactions.FirstOrDefault(c => c.Id == id);
        }

        public void Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
        }

        public void Delete(int id)
        {
            Transaction transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}