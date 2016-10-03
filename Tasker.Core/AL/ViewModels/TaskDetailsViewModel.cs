using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel, ITaskDetailsViewModel
    {
        private ITaskManager _taskManager;
        public TaskDetailsViewModel(ITaskManager taskManager) : base()
        {
            _taskManager = taskManager;
        }

        public int Id { get; set; }

        public Task GetItem()
        {
            if (Id != 0)
            {
                return _taskManager.Get(Id);
            }
            else
            {
                return new Task();
            }
        }

        public int SaveItem(Task task)
        {
            return _taskManager.SaveItem(task);
        }
    }
}
