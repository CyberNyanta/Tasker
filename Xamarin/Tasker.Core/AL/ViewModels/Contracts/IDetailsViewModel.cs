using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface IDetailsViewModel<T> where T : class, IBusinessEntity, new()
    {
        int Id { get; set; }

        T GetItem( );

        int SaveItem(T item);

        int DeleteItem(int id);
    }
}
