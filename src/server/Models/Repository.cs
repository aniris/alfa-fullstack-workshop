using System.Collections.Generic;
using System.Linq;

namespace Server.Models
{
    public static class Repository
    {
        public static List<User> Users { get; } = new List<User>();

        public static List<Card> Cards { get; } = new List<Card>();
        
        public static List<Transaction> Transactions { get; } = new List<Transaction>();

        /*public static void AddUser(User user)
        {
            Users.Add(user);
        }
        
        public static void AddCard(Card card)
        {
            Cards.Add(card);
        }
        
        public static void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }*/
    }
}