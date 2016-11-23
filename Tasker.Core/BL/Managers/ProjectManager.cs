using System.Collections.Generic;

using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class ProjectManager : IProjectManager
    {
        private IRepository<Task> _taskRepository;
        private IRepository<Project> _projectRepository;

        public ProjectManager(IUnitOfWork unitOfWork)
        {
            _taskRepository = unitOfWork.TasksRepository;
            _projectRepository = unitOfWork.ProjectsRepository;
        }

        public Project Get(int id)
        {
            return _projectRepository.GetById(id);
        }

        public List<Project> GetAll()
        {
            var projects = _projectRepository.GetAll();
            var list = new List<Project>();
            if (projects != null)
            {
                list.AddRange(projects);
            }
            return list;
        }

        public List<Task> GetProjectTasks(int projectID)
        {
            var tasks = _taskRepository.Find(x => x.ProjectID == projectID);
            var list = new List<Task>();
            if (tasks != null)
            {
                list.AddRange(tasks);
            }
            return list;
        }

        public int SaveItem(Project item)
        {
            return item != null ? _projectRepository.Save(item) : 0;
        }

        /// <exception cref="System.Exception"> - Thrown when delete transaction failed</exception>
        public int Delete(int id)
        {
            _taskRepository.DeleteGroupBy(x => x.ProjectID == id);
            return id!=0? _projectRepository.Delete(id):0;
        }

        public int Delete(Project item)
        {
            return Delete(item.ID);
        }

        public int DeleteGroup(IList<Project> group)
        {
            foreach (var project in group)
            {
                _taskRepository.DeleteGroupBy(x => x.ProjectID == project.ID);
            }
            return _projectRepository.DeleteGroup(group);
        }
    }
}
