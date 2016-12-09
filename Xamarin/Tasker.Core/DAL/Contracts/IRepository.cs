using System;
using System.Collections.Generic;

namespace Tasker.Core.DAL.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);

        int Save(T item);

        int Delete(int id);

        int Delete(T item);

        int DeleteGroupBy(Func<T, bool> predicate);

        int DeleteGroup(IList<T> group);
    }
}
