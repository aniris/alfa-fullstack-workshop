using Server.Core;
using Server.Models;

namespace Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Card> _cardRepository;
        private IRepository<Transaction> _transactionRepository;
        private IRepository<User> _userRepository;
        private readonly SQLContext _context;

        public UnitOfWork(SQLContext context)
        {
            _context = context;
        }

        public IRepository<Card> Cards
        {
            get
            {
                if (_cardRepository == null)
                    _cardRepository = new CardRepository(_context);
                return _cardRepository;
            }
        }
        
        public IRepository<Transaction> Transactions
        {
            get
            {
                if (_transactionRepository == null)
                    _transactionRepository = new TransactionRepository(_context);
                return _transactionRepository;
            }
        }
        
        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }
    }
}