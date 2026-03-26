using System;
using System.Collections.Generic;
using System.Text;
using Entity;
namespace UseCase
{
    public class ContactListManager
    {
        private readonly IContactRepository _contactRepository;

        public ContactListManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _contactRepository.GetContact();
        }

        public Contact GetContactByID(int id)
        {
            return _contactRepository.GetByID(id);
        }

        public void AddContact(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new ArgumentException("Tên không được để trống.");
            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new ArgumentException("Email không được để trống.");

            _contactRepository.Add(contact);
        }

        public void DeleteContact(int id)
        {
            var contact = _contactRepository.GetByID(id);
            if (contact == null)
                throw new Exception($"Không tìm thấy liên hệ với ID = {id}");

            _contactRepository.Delete(id);
        }

    }
}
