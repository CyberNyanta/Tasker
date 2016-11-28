using System;
using System.Collections.Generic;

using Tasker.Core.DAL.Entities;

namespace Tasker.Core.BL.Contracts
{
    public interface ITaskManager : IEntityManager<Task>
    {
        List<Task> GetProjectTasks(int projectId);

        List<Task> GetProjectOpenTasks(int projectId);

        List<Task> GetProjectCompletedTasks(int projectId);

        List<Task> GetAllOpen();

        List<Task> GetAllCompleted();

        List<Task> GetWhere(Predicate<Task> predicate);

        List<Project> GetProjects();

        List<Task> GetForToday();

        List<Task> GetForTomorrow();

        List<Task> GetForNextWeek();

        void ChangeStatus(int id);

        void ChangeStatus(Task task);
    }
}
