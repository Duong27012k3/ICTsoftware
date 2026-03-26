using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public PostgresServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Service> GetServices()
        {
            return _context.Services
                .Include(s => s.Field).ThenInclude(f => f.FieldTrans)
                .Include(s => s.ServiceTrans)
                .OrderByDescending(s => s.CreatedAt)
                .ToList();
        }

        public IEnumerable<Service> GetActiveServices()
        {
            return _context.Services
                .Include(s => s.Field).ThenInclude(f => f.FieldTrans)
                .Include(s => s.ServiceTrans)
                .Where(s => s.Status == "active")
                .OrderByDescending(s => s.CreatedAt)
                .ToList();
        }

        public IEnumerable<Service> GetByFieldID(int fieldId)
        {
            return _context.Services
                .Include(s => s.ServiceTrans)
                .Where(s => s.FieldId == fieldId)
                .ToList();
        }

        public Service GetByID(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Services
                .Include(s => s.Field).ThenInclude(f => f.FieldTrans)
                .Include(s => s.ServiceTrans)
                .FirstOrDefault(s => s.ServiceId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void Update(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }
    }
}

