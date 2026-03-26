using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IFeatureRepository
    {
        IEnumerable<Feature> GetByProjectID(int projectId);
        IEnumerable<Feature> GetByProjectAndLang(int projectId, string langCode);
        Feature GetByID(int id);
        void Add(Feature feature);
        void Update(Feature feature);
        void Delete(int id);
    }
}
