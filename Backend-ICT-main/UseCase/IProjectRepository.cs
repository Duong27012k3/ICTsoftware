using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<Project> GetActiveProjects();
        IEnumerable<Project> GetByFieldID(int fieldId);
        Project GetByID(int id);
        void Add(Project project);
        void Update(Project project);
        void Delete(int id);
    }
}
