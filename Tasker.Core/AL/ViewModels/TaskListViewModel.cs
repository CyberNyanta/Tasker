using System;
using System.Collections.Generic;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class TaskListViewModel : BaseViewModel, ITaskListViewModel
    {
        private ITaskManager _taskManager;
        private IProjectManager _projectManager;
        private bool _isSolvedTaskDisplayed = false;

        public bool IsSolvedTaskDisplayed
        {
            get { return _isSolvedTaskDisplayed; }
            set
            {
                RaiseOnCollectionChanged();
               _isSolvedTaskDisplayed = value;
            }
        }

        public event Action OnCollectionChanged;

        public TaskListViewModel(ITaskManager taskManager, IProjectManager projectManager) : base()
        {
            _taskManager = taskManager;
            _projectManager = projectManager;
        }

        public void DeleteGroup(IList<Task> group) //unused in droid
        {
            _taskManager.DeleteGroup(group);
            RaiseOnCollectionChanged();
        }

        public List<Task> GetAll()
        {
            return IsSolvedTaskDisplayed ? _taskManager.GetAll() : _taskManager.GetAllOpen();
        }

        public List<Task> GetWhere(Predicate<Task> predicate) //unused in droid
        {
            throw new NotImplementedException();
        }

        public List<Task> GetProjectTasks(int projectId)
        {
            return IsSolvedTaskDisplayed ? _taskManager.GetProjectTasks(projectId) : _taskManager.GetProjectOpenTasks(projectId);
        }

        public List<Project> GetAllProjects()
        {
            return _projectManager.GetAll();
        }

        public void ChangeStatus(Task task)
        {
            _taskManager.ChangeStatus(task);
            RaiseOnCollectionChanged();
        }

        public void ChangeStatus(int id)
        {
            _taskManager.ChangeStatus(id);
            RaiseOnCollectionChanged();
        }

        protected virtual void RaiseOnCollectionChanged()
        {
            OnCollectionChanged?.Invoke();
        }

        public List<Task> GetProjectOpenTasks(int projectId)
        {
            return _taskManager.GetProjectOpenTasks(projectId);
        }

        public List<Task> GetProjectSolveTasks(int projectId)
        {
            return _taskManager.GetProjectSolveTasks(projectId);
        }

        public List<Task> GetAllOpen()
        {
            return _taskManager.GetAllOpen();
        }

        public List<Task> GetAllSolve()
        {
            return _taskManager.GetAllSolve();
        }
    }
}
