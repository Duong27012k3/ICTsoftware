using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class FieldTransListManager
    {
        private readonly IFieldTransRepository _fieldTransRepository;

        public FieldTransListManager(IFieldTransRepository fieldTransRepository)
        {
            _fieldTransRepository = fieldTransRepository;
        }

        public IEnumerable<FieldTrans> GetTransByFieldID(int fieldId)
        {
            return _fieldTransRepository.GetByFieldID(fieldId);
        }

        public FieldTrans GetTransByID(int id)
        {
            return _fieldTransRepository.GetByID(id);
        }

        public void AddTrans(FieldTrans fieldTrans)
        {
            if (string.IsNullOrWhiteSpace(fieldTrans.Name))
                throw new ArgumentException("Ten khong duoc de trong.");
            if (string.IsNullOrWhiteSpace(fieldTrans.LangCode))
                throw new ArgumentException("Ma ngon ngu khong duoc de trong.");

            _fieldTransRepository.Add(fieldTrans);
        }

        public void UpdateTrans(FieldTrans fieldTrans)
        {
            var existing = _fieldTransRepository.GetByID(fieldTrans.FieldTransId);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich.");

            existing.Name = fieldTrans.Name;
            existing.Description = fieldTrans.Description;
            existing.LangCode = fieldTrans.LangCode;
            _fieldTransRepository.Update(existing);
        }

        public void DeleteTrans(int id)
        {
            var existing = _fieldTransRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich.");

            _fieldTransRepository.Delete(id);
        }
    }
}
