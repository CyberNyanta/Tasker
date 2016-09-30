using Tasker.Core.DAL.Entities;

namespace Tasker.Core.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Project> Projects { get; }

        IRepository<Task> Tasks { get; }    
    }
}
