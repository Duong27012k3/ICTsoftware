using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresProjectTransRepository : IProjectTransRepository
    {
        private readonly AppDbContext _context;

        public PostgresProjectTransRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProjectTrans> GetByProjectID(int projectId)
        {
            return _context.ProjectTrans
                .Where(pt => pt.ProjectId == projectId)
                .ToList();
        }

        public ProjectTrans? GetByID(int id)
        {
            return _context.ProjectTrans.Find(id);
        }

        public void Add(ProjectTrans projectTrans)
        {
            _context.ProjectTrans.Add(projectTrans);
            _context.SaveChanges();
        }

        public void Update(ProjectTrans projectTrans)
        {
            _context.ProjectTrans.Update(projectTrans);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var trans = _context.ProjectTrans.Find(id);
            if (trans != null)
            {
                _context.ProjectTrans.Remove(trans);
                _context.SaveChanges();
            }
        }
    }
}

