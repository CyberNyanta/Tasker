using System.Collections.Generic;

using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class ProjectManager:IProjectManager
    {
        private IRepository<Task> _taskRepository;
        private IRepository<Project> _projectRepository;

        public ProjectManager(IUnitOfWork unitOfWork)
        {
            _taskRepository = unitOfWork.Tasks;
            _projectRepository = unitOfWork.Projects;
        }

        public Project Get(int id)
        {
            return _projectRepository.GetById(id);
        }

        public List<Project> GetAll()
        {
            return new List<Project>(_projectRepository.GetAll());
        }

        public List<Task> GetProjectTasks(int projectID)
        {
            return new List<Task>(_taskRepository.Find(x => x.ProjectID == projectID));
        }

        public int SaveItem(Project item)
        {
            return _projectRepository.Save(item);
        }
        /// <exception cref="System.Exception"> - Thrown when delete transaction failed</exception>
        public int Delete(int id)
        {
            _taskRepository.DeleteGroupBy(x => x.ProjectID == id);
            return _projectRepository.Delete(id);
        }

        public int Delete(Project item)
        {
            return Delete(item.ID);
        }

        public int DeleteGroup(IList<Project> group)
        {
            foreach(var project in group)
            {
                _taskRepository.DeleteGroupBy(x => x.ProjectID == project.ID);
            }
            return _projectRepository.DeleteGroup(group);
        }
    }
}
