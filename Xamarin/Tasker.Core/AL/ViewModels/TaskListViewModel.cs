﻿using System;
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

        public bool IsCompletedTaskDisplayed
        {
            get { return _isSolvedTaskDisplayed; }
            set
            {
                RaiseOnCollectionChanged();
               _isSolvedTaskDisplayed = value;
            }
        }

        public int Id{ get; set; }

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
            return _taskManager.GetAll();
        }

        public List<Task> GetWhere(Predicate<Task> predicate) //unused in droid
        {
            throw new NotImplementedException();
        }

        public List<Task> GetProjectTasks(int projectId)
        {
            return _taskManager.GetProjectTasks(projectId);
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

        public List<Task> GetProjectCompletedTasks(int projectId)
        {
            return _taskManager.GetProjectCompletedTasks(projectId);
        }

        public List<Task> GetAllOpen()
        {
            return _taskManager.GetAllOpen();
        }

        public List<Task> GetAllCompleted()
        {
            return _taskManager.GetAllCompleted();
        }

        public int DeleteItem(int id)
        {
            return _taskManager.Delete(id);
        }

        public Task GetItem(int id)
        {
            return _taskManager.Get(id);
        }

        public int SaveItem(Task item)
        {
            return _taskManager.SaveItem(item);
        }

        public List<Task> GetForToday()
        {
            return _taskManager.GetForToday();
        }

        public List<Task> GetForTomorrow()
        {
            return _taskManager.GetForTomorrow();
        }

        public List<Task> GetForNextWeek()
        {
            return _taskManager.GetForNextWeek();
        }
    }
}
