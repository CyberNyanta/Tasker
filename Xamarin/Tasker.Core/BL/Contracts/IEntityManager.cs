using System.Collections.Generic;

using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.BL.Contracts
{
    public interface IEntityManager<T> where T : IBusinessEntity
    {
        T Get(int id);

        List<T> GetAll();

        int SaveItem(T item);

        int Delete(int id);

        int Delete(T item);

        int DeleteGroup(IList<T> group);
    }
}
