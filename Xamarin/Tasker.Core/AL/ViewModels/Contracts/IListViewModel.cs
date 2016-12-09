using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface IListViewModel<T> where T : class, IBusinessEntity, new()
    {
        event Action OnCollectionChanged;

        int Id { get; set; }

        List<T> GetAll();

        void DeleteGroup(IList<T> group);

        int DeleteItem(int id);

        T GetItem(int id);

    }
}
