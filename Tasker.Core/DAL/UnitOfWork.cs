using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DL;
using Tasker.Core.DL.Entities;

namespace Tasker.Core.DAL
{
    class UnitOfWork : Disposable, IUnitOfWork
    {
        TaskDatabase db;
        IRepository<Project> projectRepository;
        IRepository<DL.Entities.Task> tasksRepository;

        public UnitOfWork(string path)
        {
            db = new TaskDatabase(path);
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
