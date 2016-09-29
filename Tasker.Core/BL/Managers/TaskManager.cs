using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tasker.Core.DAL;
using Tasker.Core.DL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class TaskManeger
    {
        TaskRepository taskRepository;
        ProjectsRepository projectRepository;

        public TaskManeger(string dbPatch)
        {
            taskRepository = new TaskRepository(dbPatch);
            projectRepository = new ProjectsRepository(dbPatch);
        }

        public Task GetTask(int id)
        {            
            return taskRepository.GetById(id);
        }

        public IList<Task> GetTasks()
        {
            return new List<Task>(taskRepository.GetAll());
        }

        public IList<Task> GetProjectTasks(int projectID)
        {
            return new List<Task>(taskRepository.Find(x=>x.ProjectID == projectID));
        }

        public int SaveTask(Task item)
        {
            return taskRepository.Save(item);
        }

        public int DeleteTask(int id)
        {
            return taskRepository.Delete(id);
        }



    }
}
