using Tasker.Core.DAL.Entities;

namespace Tasker.Core.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Project> ProjectsRepository { get; }

        IRepository<Task> TasksRepository { get; }    
    }
}
