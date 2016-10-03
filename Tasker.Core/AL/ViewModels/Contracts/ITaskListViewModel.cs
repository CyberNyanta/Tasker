using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface ITaskListViewModel: IListViewModel<Task>
    {
        void SolveGroup(IList<Task> task);

        bool IsSolvedTaskDisplayed { get; set; }
    }
}
