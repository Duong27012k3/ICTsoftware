using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;
using Entity;

namespace Infrastructure
{
    public class PostgresFieldRepository : IFieldRepository
    {
        private readonly AppDbContext _context;

        public PostgresFieldRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Field> GetFields()
        {
            return _context.Fields
                .Include(f => f.FieldTrans)
                .Include(f => f.Services)
                .Include(f => f.Projects)
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }

        public IEnumerable<Field> GetActiveFields()
        {
            return _context.Fields
                .Include(f => f.FieldTrans)
                .Where(f => f.Status == "active")
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }

        public Field GetByID(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Fields
                .Include(f => f.FieldTrans)
                .Include(f => f.Services)
                .Include(f => f.Projects)
                .FirstOrDefault(f => f.FieldId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Field GetByUid(string uid)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Fields
                .Include(f => f.FieldTrans)
                .FirstOrDefault(f => f.Uid == uid);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(Field field)
        {
            _context.Fields.Add(field);
            _context.SaveChanges();
        }

        public void Update(Field field)
        {
            _context.Fields.Update(field);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var field = _context.Fields.Find(id);
            if (field != null)
            {
                _context.Fields.Remove(field);
                _context.SaveChanges();
            }
        }

        public bool UidExists(string uid, int excludeId = 0)
        {
            return _context.Fields
                .Any(f => f.Uid == uid && f.FieldId != excludeId);
        }
    }
}

