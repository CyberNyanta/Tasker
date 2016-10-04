using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel, ITaskDetailsViewModel
    {
        private ITaskManager _taskManager;
        public int Id { get; set; }

        public TaskDetailsViewModel(ITaskManager taskManager) : base()
        {
            _taskManager = taskManager;
        }

        public Task GetItem()
        {
            return Id != 0 ? _taskManager.Get(Id) : null;
        }

        public int SaveItem(Task task)
        {
            return _taskManager.SaveItem(task);
        }

        public void ChangeStatus(Task task)
        {
            _taskManager.ChangeStatus(task);
        }

        public void ChangeStatus(int id)
        {
            _taskManager.ChangeStatus(id);
        }
    }
}
