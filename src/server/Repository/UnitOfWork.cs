using Server.Core;

namespace Server.Repository
{
    public class UnitOfWork
    {
        private CardRepository _cardRepository;
        private TransactionRepository _transactionRepository;
        private UserRepository _userRepository;
        private readonly SQLContext _context;

        public UnitOfWork(SQLContext context)
        {
            _context = context;
        }

        public CardRepository Cards
        {
            get
            {
                if (_cardRepository == null)
                    _cardRepository = new CardRepository(_context);
                return _cardRepository;
            }
        }
        
        public TransactionRepository Transactions
        {
            get
            {
                if (_transactionRepository == null)
                    _transactionRepository = new TransactionRepository(_context);
                return _transactionRepository;
            }
        }
        
        public UserRepository Users
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