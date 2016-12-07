//using SQLite;
using Tasker.Core.BL.Contracts;

namespace Tasker.Core.DAL.Contracts
{
    public abstract class BaseBusinessEntity : IBusinessEntity
    {
        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
       
    }
}
