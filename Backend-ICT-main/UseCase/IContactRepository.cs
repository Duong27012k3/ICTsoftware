using System.Collections.Generic;
using Entity;

namespace UseCase
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContact();
        Contact GetByID(int id);
        void Add(Contact contact);
        void Delete(int id);
    }
}
