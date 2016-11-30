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
            _taskRepository = unitOfWork.TasksRepository;
            _projectRepository = unitOfWork.ProjectsRepository;
        }

        public Task Get(int id)
        {
            return _taskRepository.GetById(id);
        }

        public List<Task> GetAll()
        {
            var tasks = _taskRepository.GetAll();
            var list = new List<Task>();
            if (tasks != null)
            {
                list.AddRange(tasks);
            }
            return list;
        }

        public List<Task> GetAllOpen()
        {
            return GetAll().FindAll(t => !t.IsCompleted);
        }

        public List<Task> GetAllCompleted()
        {
            return GetAll().FindAll(t => t.IsCompleted);
        }

        public List<Task> GetProjectTasks(int projectId)
        {
            var tasks = _taskRepository.Find(x => x.ProjectID == projectId);
            var list = new List<Task>();
            if (tasks != null)
            {
                list.AddRange(tasks);
            }
            return list;
        }

        public List<Task> GetProjectOpenTasks(int projectId)
        {
            return GetProjectTasks(projectId).FindAll(t => !t.IsCompleted);
        }

        public List<Task> GetProjectCompletedTasks(int projectId)
        {
            return GetProjectTasks(projectId).FindAll(t => t.IsCompleted);
        }

        public List<Task> GetWhere(Predicate<Task> predicate)
        {
            return GetAll().FindAll(predicate);
        }

        public List<Project> GetProjects()
        {
            var projects = _projectRepository.GetAll();
            var list = new List<Project>();
            if (projects != null)
            {
                list.AddRange(projects);
            }
            return list;
        }

        public List<Task> GetForToday()
        {
            return GetAllOpen().FindAll(t => t.DueDate < DateTime.Today.AddDays(1));
        }

        public List<Task> GetForTomorrow()
        {
            return GetAllOpen().FindAll(t => t.DueDate < DateTime.Today.AddDays(2) && t.DueDate >= DateTime.Today.AddDays(1));
        }

        public List<Task> GetForNextWeek()
        {
            return GetAllOpen().FindAll(t => t.DueDate < DateTime.Today.AddDays(8));
        }

        public int SaveItem(Task item)
        {
            if (item.ID != 0)
            {
                var previousVersion = _taskRepository.GetById(item.ID);
                if (item.ProjectID != previousVersion.ProjectID)
                {
                    if(previousVersion.ProjectID != 0)
                    {
                        var previousProject = _projectRepository.GetById(previousVersion.ProjectID);
                        if (previousVersion.IsCompleted)
                        {
                            previousProject.CountOfCompletedTasks--;
                        }
                        else
                        {
                            previousProject.CountOfOpenTasks--;
                        }
                        _projectRepository.Save(previousProject);
                    }
                    if (item.ProjectID != 0)
                    {
                        var project = _projectRepository.GetById(item.ProjectID);
                        if (!item.IsCompleted)
                        {
                            project.CountOfOpenTasks++;
                        }
                        else
                        {
                            project.CountOfCompletedTasks++;
                        }

                        _projectRepository.Save(project);
                    }
                }
            }
            else if (item.ProjectID != 0 )
            {
                var project = _projectRepository.GetById(item.ProjectID);
                if (!item.IsCompleted)
                {
                    project.CountOfOpenTasks++;
                }
                else
                {
                    project.CountOfCompletedTasks++;
                }
               
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
                if (!task.IsCompleted)
                {
                    
                    project.CountOfCompletedTasks++;
                    project.CountOfOpenTasks--;
                }
                else
                {
                    project.CountOfCompletedTasks--;
                    project.CountOfOpenTasks++;
                }
                _projectRepository.Save(project);
            }
            task.CompletedDate = DateTime.Now;
            task.IsCompleted = !task.IsCompleted;
            _taskRepository.Save(task);
        }

        public int Delete(int id)
        {
            var task = Get(id);
            if (task.ProjectID != 0)
            {
                var project = _projectRepository.GetById(task.ProjectID);

                if (task.IsCompleted)
                {
                    project.CountOfCompletedTasks--;
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
