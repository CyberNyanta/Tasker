using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Tasker.Core.DataLayer;
using System.IO;
using Tasker.Core.DataLayer.Entities;
using Tasker.Core.BussinessLogic.Contracts;

namespace Tasker.Core.DataAccessLayer
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IBusinessEntity, new()
    {
        static TaskDatabase db;

        public BaseRepository(string dbLocation)
        {
            if(db==null)
            db = new TaskDatabase(dbLocation);
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
