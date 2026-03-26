using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IFieldTransRepository
    {

        IEnumerable<FieldTrans> GetByFieldID(int fieldId);
        FieldTrans GetByID(int id);
        void Add(FieldTrans fieldTrans);
        void Update(FieldTrans fieldTrans);
        void Delete(int id);
    }
}
