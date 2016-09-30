using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.DAL;
using Tasker.Core.DL;
using Tasker.Core.DL.Entities;

namespace Tasker.Core.BL.Contracts
{
    public interface IUnitOfWork
    {

        IRepository<Project> Projects { get; }

        IRepository<Task> Tasks { get; }

    }
}
