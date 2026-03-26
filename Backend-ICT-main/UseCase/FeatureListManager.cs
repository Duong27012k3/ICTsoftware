using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class FeatureListManager
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureListManager(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public IEnumerable<Feature> GetFeaturesByProject(int projectId)
        {
            return _featureRepository.GetByProjectID(projectId);
        }

        public IEnumerable<Feature> GetFeaturesByProjectAndLang(int projectId, string langCode)
        {
            return _featureRepository.GetByProjectAndLang(projectId, langCode);
        }

        public Feature GetFeatureByID(int id)
        {
            return _featureRepository.GetByID(id);
        }

        public void AddFeature(Feature feature)
        {
            if (feature.ProjectId <= 0)
                throw new ArgumentException("Du an khong hop le.");
            if (string.IsNullOrWhiteSpace(feature.LangCode))
                throw new ArgumentException("Ma ngon ngu khong duoc de trong.");

            _featureRepository.Add(feature);
        }

        public void UpdateFeature(Feature feature)
        {
            var existing = _featureRepository.GetByID(feature.FeatureId);
            if (existing == null)
                throw new Exception("Khong tim thay tinh nang.");

            existing.Content = feature.Content;
            existing.Icon = feature.Icon;
            existing.LangCode = feature.LangCode;
            _featureRepository.Update(existing);
        }

        public void DeleteFeature(int id)
        {
            var existing = _featureRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay tinh nang.");

            _featureRepository.Delete(id);
        }
    }
}
