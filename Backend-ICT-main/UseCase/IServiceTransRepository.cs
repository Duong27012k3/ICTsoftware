using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IServiceTransRepository
    {
        IEnumerable<ServiceTrans> GetByServiceID(int serviceId);
        ServiceTrans? GetByID(int id);
        void Add(ServiceTrans serviceTrans);
        void Update(ServiceTrans serviceTrans);
        void Delete(int id);
    }
}
