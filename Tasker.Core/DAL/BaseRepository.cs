using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Tasker.Core.DL;
using System.IO;
using Tasker.Core.DL.Entities;
using Tasker.Core.BL.Contracts;

namespace Tasker.Core.DAL
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IBusinessEntity, new()
    {
        TaskDatabase db;

        public BaseRepository(TaskDatabase db)
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
