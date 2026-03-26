using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class ServiceListManager
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceListManager(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IEnumerable<Service> GetAllServices()
        {
            return _serviceRepository.GetServices();
        }

        public IEnumerable<Service> GetActiveServices()
        {
            return _serviceRepository.GetActiveServices();
        }

        public IEnumerable<Service> GetServicesByField(int fieldId)
        {
            return _serviceRepository.GetByFieldID(fieldId);
        }

        public Service GetServiceByID(int id)
        {
            return _serviceRepository.GetByID(id);
        }

        public void AddService(Service service)
        {
            if (service.FieldId <= 0)
                throw new ArgumentException("Linh vuc khong hop le.");

            service.CreatedAt = DateTime.Now;
            service.Status = "active";
            _serviceRepository.Add(service);
        }

        public void UpdateService(Service service)
        {
            var existing = _serviceRepository.GetByID(service.ServiceId);
            if (existing == null)
                throw new Exception("Khong tim thay dich vu.");
            existing.Name = service.Name;
            existing.Description = service.Description;
            existing.FieldId = service.FieldId;
            existing.Image = service.Image;
            existing.CatalogueUrl = service.CatalogueUrl;
            existing.Status = service.Status;
            _serviceRepository.Update(existing);
        }

        public void DeleteService(int id)
        {
            var existing = _serviceRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay dich vu.");

            _serviceRepository.Delete(id);
        }
    }
}
