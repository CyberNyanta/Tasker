using System.Collections.Generic;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Contracts
{
    public interface ITaskManager:IManager<Task>
    {
        List<Task> GetProjectTasks(int projectId);
    }
}
