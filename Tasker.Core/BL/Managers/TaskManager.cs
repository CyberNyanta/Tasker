using System;
using System.Collections.Generic;

using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Managers
{
    public class TaskManager : ITaskManager
    {
        private IRepository<Task> _taskRepository;
        private IRepository<Project> _projectRepository;

        public TaskManager(IUnitOfWork unitOfWork)
        {
            _taskRepository = unitOfWork.Tasks;
            _projectRepository = unitOfWork.Projects;
        }

        public Task Get(int id)
        {            
            return _taskRepository.GetById(id);
        }

        public List<Task> GetAll()
        {
            return new List<Task>(_taskRepository.GetAll());
        }

        public List<Task> GetProjectTasks(int projectID)
        {
            return new List<Task>(_taskRepository.Find(x=>x.ProjectID == projectID));
        }

        public int SaveItem(Task item)
        {
            if (item.ID != 0)
            {
                var project = _projectRepository.GetById(item.ProjectID);
                project.CountOfOpenTasks++;
                _projectRepository.Save(project);
            }
            return _taskRepository.Save(item);
        }

        public void ChangeStatus(int id)
        {
            var task = Get(id);
            ChangeStatus(task);
        }

        public void ChangeStatus(Task task)
        {
            if (task.ProjectID != 0)
            {
                var project = _projectRepository.GetById(task.ProjectID);
                if (!task.IsSolved)
                {
                    project.CountOfSolveTasks++;
                    project.CountOfOpenTasks--;
                }
                else
                {
                    project.CountOfSolveTasks--;
                    project.CountOfOpenTasks++;
                }
                _projectRepository.Save(project);
            }
                        
            task.IsSolved = !task.IsSolved;
            _taskRepository.Save(task);
        }
            
        public int Delete(int id)
        {
            var task = Get(id);
            if (task.ProjectID != 0)
            {
                var project = _projectRepository.GetById(task.ProjectID);

                if (task.IsSolved)
                {
                    project.CountOfSolveTasks--;
                }
                else
                {
                    project.CountOfOpenTasks--;
                }

                _projectRepository.Save(project);
            }
            
            return _taskRepository.Delete(id);
        }

        public int Delete(Task item)
        {
            return Delete(item.ID);
        }

        public int DeleteGroup(IList<Task> group)
        {
            int count = 0;
            foreach (var task in group)
            {
                Delete(task.ID);
                count++;
            }
            return count;
        }
    }
}
