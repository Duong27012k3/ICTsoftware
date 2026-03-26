using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public PostgresProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects
                .Include(p => p.Field).ThenInclude(f => f.FieldTrans)
                .Include(p => p.ProjectTrans)
                .Include(p => p.Features)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public IEnumerable<Project> GetActiveProjects()
        {
            return _context.Projects
                .Include(p => p.Field).ThenInclude(f => f.FieldTrans)
                .Include(p => p.ProjectTrans)
                .Include(p => p.Features)
                .Where(p => p.Status == "active")
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public IEnumerable<Project> GetByFieldID(int fieldId)
        {
            return _context.Projects
                .Include(p => p.ProjectTrans)
                .Where(p => p.FieldId == fieldId)
                .ToList();
        }

        public Project GetByID(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Projects
                .Include(p => p.Field).ThenInclude(f => f.FieldTrans)
                .Include(p => p.ProjectTrans)
                .Include(p => p.Features)
                .Include(p => p.Blocks).ThenInclude(b => b.BlockTrans)
                .FirstOrDefault(p => p.ProjectId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}

