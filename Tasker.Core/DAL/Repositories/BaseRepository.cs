using System;
using System.Collections.Generic;
using Tasker.Core.DL;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IBusinessEntity, new()
    {
        private TaskerDatabase db;

        public BaseRepository(TaskerDatabase db)
        {            
            this.db = db;
        }

      
        public int Save(T item)
        {           
           return db.SaveItem<T>(item);
        }

        public int Delete(T item)
        {
            return db.DeleteItem<T>(item);
        }

        public int Delete(int id)
        {
            return db.DeleteItem<T>(id);
        }
        /// <exception cref="Exception">Thrown when delete transaction failed</exception>
        public int DeleteGroupBy(Func<T,bool> predicate)
        {
            return db.DeleteGroupBy<T>(predicate);
        }
        /// <exception cref="Exception">Thrown when delete transaction failed</exception>
        public int DeleteGroup(IList<T> group)
        {
            return db.DeleteGroup<T>(group);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return db.FindAll<T>(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return db.GetItems<T>();
        }

        public T GetById(int id)
        {
            return db.GetItem<T>(id);
        }

    }
}
