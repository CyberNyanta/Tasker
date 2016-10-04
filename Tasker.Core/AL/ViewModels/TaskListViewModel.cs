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

        public bool IsSolvedTaskDisplayed { get; set; }

        public event Action OnCollectionChanged;

        public TaskListViewModel(ITaskManager taskManager) : base()
        {
            _taskManager = taskManager;
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
    }
}
