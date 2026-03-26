using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IFieldRepository
    {
        IEnumerable<Field> GetFields();
        IEnumerable<Field> GetActiveFields();
        Field GetByID(int id);
        Field GetByUid(string uid);
        void Add(Field field);
        void Update(Field field);
        void Delete(int id);
        bool UidExists(string uid, int excludeId = 0);
    }
}
