using Server.Models;

namespace Server.Repository
{
    public interface IUnitOfWork
    {
        IRepository<Card> Cards { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<User> Users { get; }
    }
}