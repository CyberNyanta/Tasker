using System.Collections.Generic;
using System.Linq;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class ProjectManager:IProjectManager
    {
        private IRepository<Task> taskRepository;
        private IRepository<Project> projectRepository;

        public ProjectManager(IUnitOfWork unitOfWork)
        {
            taskRepository = unitOfWork.Tasks;
            projectRepository = unitOfWork.Projects;
        }

        public Project Get(int id)
        {
            return projectRepository.GetById(id);
        }

        public List<Project> GetAll()
        {
            return new List<Project>(projectRepository.GetAll());
        }

        public List<Task> GetProjectTasks(int projectID)
        {
            return new List<Task>(taskRepository.Find(x => x.ProjectID == projectID));
        }

        public int SaveItem(Project item)
        {
            return projectRepository.Save(item);
        }
        /// <exception cref="System.Exception"> - Thrown when delete transaction failed</exception>
        public int Delete(int id)
        {
            taskRepository.DeleteGroupBy(x => x.ProjectID == id);
            return projectRepository.Delete(id);
        }
    }
}
