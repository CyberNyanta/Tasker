using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface ITaskListViewModel: IListViewModel<Task>
    {
        bool IsCompletedTaskDisplayed { get; set; }

        void ChangeStatus(Task task);

        void ChangeStatus(int id);

        int SaveItem(Task item);

        List<Project> GetAllProjects();

        List<Task> GetWhere(Predicate<Task> predicate);

        List<Task> GetProjectTasks(int projectId);  

        List<Task> GetProjectOpenTasks(int projectId);

        List<Task> GetProjectCompletedTasks(int projectId);

        List<Task> GetAllOpen();

        List<Task> GetAllCompleted();

        List<Task> GetForToday();

        List<Task> GetForTomorrow();

        List<Task> GetForNextWeek();

    }
}
