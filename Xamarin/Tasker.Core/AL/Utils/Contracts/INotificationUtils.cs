using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.Utils.Contracts
{
    public interface INotificationUtils
    {
        void SetTaskReminder(Task task);
        void RemoveTaskReminder(Task task);
    }
}
