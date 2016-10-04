using System.Collections.Generic;

using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Contracts
{
    public interface IProjectManager:IManager<Project>
    {
        List<Task> GetProjectTasks(int projectId);
    }
}
