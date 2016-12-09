using Tasker.Core.DL;
using Tasker.Core.DAL.Entities;
using Tasker.Core.DAL.Repositories;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private TaskerDatabase _db;
        private IRepository<Project> _projectRepository;
        private IRepository<Task> _tasksRepository;

        public UnitOfWork(IDatabasePath path)
        {
            _db = new TaskerDatabase(path.GetDatabasePath());
            _projectRepository = new ProjectsRepository(_db);
            _tasksRepository = new TaskRepository(_db);
        }

        public IRepository<Project> ProjectsRepository
        {
            get
            {
                return _projectRepository;
            }
        }

        public IRepository<Task> TasksRepository
        {
            get
            {
                return _tasksRepository;
            }
        }

        protected override void DisposeCore()
        {
            base.DisposeCore();
            _projectRepository = null;
            _tasksRepository = null;            
            _db.Dispose();
    
        }
    }
}
