using System;
using System.Collections.Generic;

using Tasker.Core.DL;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IBusinessEntity, new()
    {
        private TaskerDatabase _db;

        public BaseRepository(TaskerDatabase db)
        {            
            _db = db;
        }
      
        public int Save(T item)
        {           
           return _db.SaveItem<T>(item);
        }

        public int Delete(T item)
        {
            return _db.DeleteItem<T>(item);
        }

        public int Delete(int id)
        {
            return _db.DeleteItem<T>(id);
        }
        /// <exception cref="Exception">Thrown when delete transaction failed</exception>
        public int DeleteGroupBy(Func<T,bool> predicate)
        {
            return _db.DeleteGroupBy<T>(predicate);
        }
        /// <exception cref="Exception">Thrown when delete transaction failed</exception>
        public int DeleteGroup(IList<T> group)
        {
            return _db.DeleteGroup<T>(group);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _db.FindAll<T>(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.GetItems<T>();
        }

        public T GetById(int id)
        {
            return _db.GetItem<T>(id);
        }

    }
}
