using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DL;
using Tasker.Core.DAL.Entities;
using Tasker.Core.DAL.Repositories;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.AL.Utils;

namespace Tasker.Core.DAL
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        TaskerDatabase db;
        IRepository<Project> projectRepository;
        IRepository<Task> tasksRepository;

        public UnitOfWork(IDatabasePath path)
        {
            db = new TaskerDatabase(path.GetDatabasePath());
            projectRepository = new ProjectsRepository(db);
            tasksRepository = new TaskRepository(db);
        }

        public IRepository<Project> Projects
        {
            get
            {
                return projectRepository;
            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                return tasksRepository;
            }
        }

        protected override void DisposeCore()
        {
            base.DisposeCore();
            projectRepository = null;
            tasksRepository = null;            
            db.Dispose();
    
        }
    }
}
