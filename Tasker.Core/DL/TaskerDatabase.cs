using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Tasker.Core.DAL.Entities;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DL
{
    public class TaskerDatabase : SQLiteConnection
    {

        private static object locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasker.DataLayer.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public TaskerDatabase(string path) : base(path)
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

        /// <summary>
        /// Delete every object from Table that fits the condition of predicate.
        /// </summary>
        /// <param name="predicate">
        /// Condition for items that need to be removed.
        /// </param>
        /// <returns>
        /// Count of deleted items
        /// </returns>
        /// <exception cref="Exception">Thrown when delete transaction failed</exception>
        public int DeleteGroupBy<T>(Func<T,bool> predicate) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                var count = 0;
                BeginTransaction();   
                     
                try
                {
                    var listOfItems = FindAll<T>(predicate).ToList();
                    for (int i = 0; i < listOfItems.Count; i++, count++)
                    {
                        Delete(listOfItems[i]);
                    }

                    Commit();

                    return count;
                }
                catch (Exception ex)
                {                    
                    Rollback();
                    throw;
                }
            }
        }

        public int DeleteGroup<T>(IList<T> group) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                var count = 0;
                BeginTransaction();

                try
                {
                    for (int i = 0; i < group.Count; i++, count++)
                    {
                        Delete(group[i]);
                    }

                    Commit();

                    return count;
                }
                catch (Exception ex)
                {
                    Rollback();
                    throw;
                }
            }
        }
    }
}
