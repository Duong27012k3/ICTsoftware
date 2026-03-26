using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresServiceTransRepository : IServiceTransRepository
    {
        private readonly AppDbContext _context;

        public PostgresServiceTransRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ServiceTrans> GetByServiceID(int serviceId)
        {
            return _context.ServiceTrans
                .Where(st => st.ServiceId == serviceId)
                .ToList();
        }

        public ServiceTrans? GetByID(int id)
        {
            return _context.ServiceTrans.Find(id);
        }

        public void Add(ServiceTrans serviceTrans)
        {
            _context.ServiceTrans.Add(serviceTrans);
            _context.SaveChanges();
        }

        public void Update(ServiceTrans serviceTrans)
        {
            _context.ServiceTrans.Update(serviceTrans);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var trans = _context.ServiceTrans.Find(id);
            if (trans != null)
            {
                _context.ServiceTrans.Remove(trans);
                _context.SaveChanges();
            }
        }
    }
}
