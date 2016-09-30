using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL;
using Tasker.Core.DL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class ProjectsManager
    {
        IRepository<Task> taskRepository;
        IRepository<Project> projectRepository;

        public ProjectsManager(IUnitOfWork unitOfWork)
        {
            taskRepository = unitOfWork.Tasks;
            projectRepository = unitOfWork.Projects;
        }

        public Project GetProject(int id)
        {
            return projectRepository.GetById(id);
        }

        public IList<Project> GetProjects()
        {
            return new List<Project>(projectRepository.GetAll());
        }

        public IList<Task> GetProjectTasks(int projectID)
        {
            return new List<Task>(taskRepository.Find(x => x.ProjectID == projectID));
        }

        public int SaveProject(Project item)
        {
            return projectRepository.Save(item);
        }

        public int DeleteProject(int id)
        {
            var projectTasks = GetProjectTasks(id).ToList();
            for(int i =0; i< projectTasks.Count; i++)
            {
                taskRepository.Delete(projectTasks[i]);
            }
            return projectRepository.Delete(id);
        }
    }
}
