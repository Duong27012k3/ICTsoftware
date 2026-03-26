using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IProjectTransRepository
    {
        IEnumerable<ProjectTrans> GetByProjectID(int projectId);
        ProjectTrans? GetByID(int id);
        void Add(ProjectTrans projectTrans);
        void Update(ProjectTrans projectTrans);
        void Delete(int id);
    }
}
