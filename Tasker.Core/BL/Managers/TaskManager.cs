using System.Collections.Generic;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class TaskManager:ITaskManager
    {
        private IRepository<Task> taskRepository;
        private IRepository<Project> projectRepository;

        public TaskManager(IUnitOfWork unitOfWork)
        {
            taskRepository = unitOfWork.Tasks;
            projectRepository = unitOfWork.Projects;
        }

        public Task Get(int id)
        {            
            return taskRepository.GetById(id);
        }

        public List<Task> GetAll()
        {
            return new List<Task>(taskRepository.GetAll());
        }

        public List<Task> GetProjectTasks(int projectID)
        {
            return new List<Task>(taskRepository.Find(x=>x.ProjectID == projectID));
        }

        public int SaveItem(Task item)
        {
            return taskRepository.Save(item);
        }

        public int Delete(int id)
        {
            return taskRepository.Delete(id);
        }



    }
}
