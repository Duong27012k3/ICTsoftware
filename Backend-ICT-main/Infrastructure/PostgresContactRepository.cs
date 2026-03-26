using System.Collections.Generic;
using System.Linq;
using Entity;
using UseCase;

namespace Infrastructure
{
    public class PostgresContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public PostgresContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Contact> GetContact()
        {
            return _context.Contacts
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }

        public Contact GetByID(int id)
        {
            // Dung ContactId thay vi ID (theo dung ten property trong Entity)
            return _context.Contacts.Find(id);
        }

        public void Add(Contact contact)
        {
            contact.CreatedAt = DateTime.Now;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
        }
    }
}
