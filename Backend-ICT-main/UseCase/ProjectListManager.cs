using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class ProjectListManager
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectListManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projectRepository.GetProjects();
        }

        public IEnumerable<Project> GetActiveProjects()
        {
            return _projectRepository.GetActiveProjects();
        }

        public IEnumerable<Project> GetProjectsByField(int fieldId)
        {
            return _projectRepository.GetByFieldID(fieldId);
        }

        public Project GetProjectByID(int id)
        {
            return _projectRepository.GetByID(id);
        }

        public void AddProject(Project project)
        {
            if (project.FieldId <= 0)
                throw new ArgumentException("Linh vuc khong hop le.");

            project.CreatedAt = DateTime.Now;
            project.Status = "active";
            _projectRepository.Add(project);
        }

        public void UpdateProject(Project project)
        {
            var existing = _projectRepository.GetByID(project.ProjectId);
            if (existing == null)
                throw new Exception("Khong tim thay du an.");
            existing.Name = project.Name;
            existing.Description = project.Description;
            existing.FieldId = project.FieldId;
            existing.Image = project.Image;
            existing.CatalogueUrl = project.CatalogueUrl;
            existing.Status = project.Status;
            _projectRepository.Update(existing);
        }

        public void DeleteProject(int id)
        {
            var existing = _projectRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay du an.");

            _projectRepository.Delete(id);
        }
    }
}
