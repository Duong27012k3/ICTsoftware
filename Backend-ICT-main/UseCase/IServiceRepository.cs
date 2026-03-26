using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetServices();
        IEnumerable<Service> GetActiveServices();
        IEnumerable<Service> GetByFieldID(int fieldId);
        Service GetByID(int id);
        void Add(Service service);
        void Update(Service service);
        void Delete(int id);

    }
}
