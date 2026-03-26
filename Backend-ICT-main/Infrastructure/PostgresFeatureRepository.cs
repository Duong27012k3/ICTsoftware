using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresFeatureRepository : IFeatureRepository
    {
        private readonly AppDbContext _context;

        public PostgresFeatureRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feature> GetByProjectID(int projectId)
        {
            return _context.Features
                .Where(f => f.ProjectId == projectId)
                .ToList();
        }

        public IEnumerable<Feature> GetByProjectAndLang(int projectId, string langCode)
        {
            return _context.Features
                .Where(f => f.ProjectId == projectId && f.LangCode == langCode)
                .ToList();
        }

        public Feature GetByID(int id)
        {
            return _context.Features.Find(id);
        }

        public void Add(Feature feature)
        {
            _context.Features.Add(feature);
            _context.SaveChanges();
        }

        public void Update(Feature feature)
        {
            _context.Features.Update(feature);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var feature = _context.Features.Find(id);
            if (feature != null)
            {
                _context.Features.Remove(feature);
                _context.SaveChanges();
            }
        }
    }
}

