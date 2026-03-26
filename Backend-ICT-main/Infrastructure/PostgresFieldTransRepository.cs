using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresFieldTransRepository : IFieldTransRepository
    {
        private readonly AppDbContext _context;

        public PostgresFieldTransRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FieldTrans> GetByFieldID(int fieldId)
        {
            return _context.FieldTrans
                .Where(ft => ft.FieldId == fieldId)
                .ToList();
        }

        public FieldTrans GetByID(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.FieldTrans.Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(FieldTrans fieldTrans)
        {
            _context.FieldTrans.Add(fieldTrans);
            _context.SaveChanges();
        }

        public void Update(FieldTrans fieldTrans)
        {
            _context.FieldTrans.Update(fieldTrans);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var trans = _context.FieldTrans.Find(id);
            if (trans != null)
            {
                _context.FieldTrans.Remove(trans);
                _context.SaveChanges();
            }
        }
    }
}

