using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class FieldListManager
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldListManager(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public IEnumerable<Field> GetAllFields()
        {
            return _fieldRepository.GetFields();
        }

        public IEnumerable<Field> GetActiveFields()
        {
            return _fieldRepository.GetActiveFields();
        }

        public Field GetFieldByID(int id)
        {
            return _fieldRepository.GetByID(id);
        }

        public void AddField(Field field)
        {
            if (string.IsNullOrWhiteSpace(field.Uid))
                throw new ArgumentException("UID khong duoc de trong.");
            if (_fieldRepository.UidExists(field.Uid))
                throw new Exception("UID da ton tai.");

            field.CreatedAt = DateTime.Now;
            _fieldRepository.Add(field);
        }

        public void UpdateField(Field field)
        {
            var existing = _fieldRepository.GetByID(field.FieldId);
            if (existing == null)
                throw new Exception("Khong tim thay linh vuc.");
            if (_fieldRepository.UidExists(field.Uid, field.FieldId))
                throw new Exception("UID da ton tai.");
            existing.NameField = field.NameField;
            existing.Uid = field.Uid;
            existing.Image = field.Image;
            existing.Status = field.Status;
            _fieldRepository.Update(existing);
        }

        public void DeleteField(int id)
        {
            var existing = _fieldRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay linh vuc.");

            _fieldRepository.Delete(id);
        }
    }
}
