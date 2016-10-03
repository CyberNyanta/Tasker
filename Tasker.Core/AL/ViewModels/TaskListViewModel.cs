using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class TaskListViewModel : BaseViewModel, ITaskListViewModel
    {
        private ITaskManager _taskManager;
        public TaskListViewModel(ITaskManager taskManager) : base()
        {
            _taskManager = taskManager;
        }

        public bool IsSolvedTaskDisplayed { get; set; }

        public event Action OnCollectionChanged;

        public void DeleteGroup(IList<Task> group)
        {
            _taskManager.DeleteGroup(group);
            RaiseOnCollectionChanged();
        }

        public List<Task> GetAll()
        {
            return _taskManager.GetAll();
        }

        public void SolveGroup(IList<Task> tasks)
        {
            foreach(var task in tasks)
            {
                task.IsSolved = true;
                _taskManager.SaveItem(task);
            }
            RaiseOnCollectionChanged();
        }

        protected virtual void RaiseOnCollectionChanged()
        {
            OnCollectionChanged?.Invoke();
        }
    }
}
