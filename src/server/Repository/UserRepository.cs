using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Server.Core;
using Server.Models;

namespace Server.Repository
{
    public class UserRepository: IRepository<User>
    {
        private readonly SQLContext _context;

        public UserRepository(SQLContext context)
        {
            _context = context;
        }
        
        public IEnumerable<User> GetAll()
        {
            return (IEnumerable<User>)_context.Users.Include(u => u.Cards).ThenInclude(c => c.Transactions).ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Include(u => u.Cards).ThenInclude(c => c.Transactions).FirstOrDefault(c => c.Id == id);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}