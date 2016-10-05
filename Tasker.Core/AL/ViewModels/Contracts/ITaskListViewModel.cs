using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface ITaskListViewModel: IListViewModel<Task>
    {
        bool IsSolvedTaskDisplayed { get; set; }

        void ChangeStatus(Task task);

        void ChangeStatus(int id);
    }
}
