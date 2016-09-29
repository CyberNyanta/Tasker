using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Tasker.Core.DL.Entities;
using Tasker.Core.BL.Contracts;

namespace Tasker.Core.DL
{
    public class TaskDatabase: SQLiteConnection
    {

        static object locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasker.DataLayer.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public TaskDatabase(string path) : base(path)
        {
                        
            CreateTable<Task>();
            CreateTable<Project>();
           
        }

        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return (from i in Table<T>() select i).ToList();
            }
        }


        public IEnumerable<T> FindAll<T>(Func<T, bool> predicate) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return Table<T>().Where<T>(predicate);              
            }
        }
        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return Table<T>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    Update(item);
                    return item.ID;
                }
                else
                {
                    return Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return Delete(new T() { ID = id });
            }
        }

        public int DeleteItem<T>(T item) where T : IBusinessEntity
        {
            return DeleteItem(item);
        }
    }
}
