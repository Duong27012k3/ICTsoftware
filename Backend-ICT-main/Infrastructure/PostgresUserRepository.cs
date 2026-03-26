using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public PostgresUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetByID(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                throw new InvalidOperationException($"User with ID {id} not found.");
            return user;
        }

        public User GetByUsername(string username)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Users
                .FirstOrDefault(u => u.Username == username);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
    }
}
