using System.Collections.Generic;
using System.Linq;
using Server.Core;
using Server.Models;

namespace Server.Repository
{
    public class CardRepository : IRepository<Card>
    {
        private readonly SQLContext _context;

        public CardRepository(SQLContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Card> GetAll()
        {
            return _context.Cards.ToList();
        }

        public Card GetById(int id)
        {
            return _context.Cards.FirstOrDefault(c => c.Id == id);
        }

        public Card GetByNumber(string number)
        {
            return _context.Cards.FirstOrDefault(c => c.CardNumber == number);
        }
        
        

        public void Create(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public void Update(Card card)
        {
            _context.Cards.Update(card);
        }

        public void Delete(int id)
        {
            Card card = _context.Cards.FirstOrDefault(c => c.Id == id);

            if (card != null)
            {
                _context.Cards.Remove(card);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}