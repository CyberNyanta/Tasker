using System.Collections.Generic;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface ITaskDetailsViewModel: IDetailsViewModel<Task>
    {
        void ChangeStatus(Task task);

        void ChangeStatus(int id);

        List<Project> GetProjects();
    }
}
