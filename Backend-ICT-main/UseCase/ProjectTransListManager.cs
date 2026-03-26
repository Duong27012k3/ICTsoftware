using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class ProjectTransListManager
    {
        private readonly IProjectTransRepository _projectTransRepository;

        public ProjectTransListManager(IProjectTransRepository projectTransRepository)
        {
            _projectTransRepository = projectTransRepository;
        }

        public IEnumerable<ProjectTrans> GetTransByProjectID(int projectId)
        {
            return _projectTransRepository.GetByProjectID(projectId);
        }

        public ProjectTrans GetTransByID(int id)
        {
            return _projectTransRepository.GetByID(id);
        }

        public void AddTrans(ProjectTrans projectTrans)
        {
            if (string.IsNullOrWhiteSpace(projectTrans.Name))
                throw new ArgumentException("Ten du an khong duoc de trong.");
            if (string.IsNullOrWhiteSpace(projectTrans.LangCode))
                throw new ArgumentException("Ma ngon ngu khong duoc de trong.");

            _projectTransRepository.Add(projectTrans);
        }

        public void UpdateTrans(ProjectTrans projectTrans)
        {
            var existing = _projectTransRepository.GetByID(projectTrans.ProjectTransId);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich.");

            existing.Name = projectTrans.Name;
            existing.ShortDescription = projectTrans.ShortDescription;
            existing.LangCode = projectTrans.LangCode;
            _projectTransRepository.Update(existing);
        }

        public void DeleteTrans(int id)
        {
            var existing = _projectTransRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich.");

            _projectTransRepository.Delete(id);
        }
    }
}
