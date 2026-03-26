using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class ServiceTransListManager
    {
        private readonly IServiceTransRepository _serviceTransRepository;

        public ServiceTransListManager(IServiceTransRepository serviceTransRepository)
        {
            _serviceTransRepository = serviceTransRepository;
        }

        public IEnumerable<ServiceTrans> GetTransByServiceID(int serviceId)
        {
            return _serviceTransRepository.GetByServiceID(serviceId);
        }

        public ServiceTrans GetTransByID(int id)
        {
            return _serviceTransRepository.GetByID(id);
        }

        public void AddTrans(ServiceTrans serviceTrans)
        {
            if (string.IsNullOrWhiteSpace(serviceTrans.Name))
                throw new ArgumentException("Ten dich vu khong duoc de trong.");
            if (string.IsNullOrWhiteSpace(serviceTrans.LangCode))
                throw new ArgumentException("Ma ngon ngu khong duoc de trong.");

            _serviceTransRepository.Add(serviceTrans);
        }

        public void UpdateTrans(ServiceTrans serviceTrans)
        {
            var existing = _serviceTransRepository.GetByID(serviceTrans.ServiceTransId);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich.");

            existing.Name = serviceTrans.Name;
            existing.ShortDescription = serviceTrans.ShortDescription;
            existing.LangCode = serviceTrans.LangCode;
            _serviceTransRepository.Update(existing);
        }

        public void DeleteTrans(int id)
        {
            var existing = _serviceTransRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich.");

            _serviceTransRepository.Delete(id);
        }
    }
}
