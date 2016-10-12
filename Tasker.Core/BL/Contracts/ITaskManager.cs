using System.Collections.Generic;

using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Contracts
{
    public interface ITaskManager:IManager<Task>
    {
        List<Task> GetProjectTasks(int projectId);

        List<Task> GetProjectOpenTasks(int projectId);

        List<Task> GetAllOpen();

        void ChangeStatus(int id);

        void ChangeStatus(Task task);
    }
}
