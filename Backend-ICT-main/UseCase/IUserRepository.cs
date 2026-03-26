using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetByID(int id);
        User GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        bool UsernameExists(string username);
    }
}
