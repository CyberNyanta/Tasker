using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tasker.Core.BL.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);


        int Save(T item);

        int Delete(int id);

        int Delete(T item);
    }
}
