using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Core.BL.Contracts
{
    public abstract class BaseBusinessEntity : IBusinessEntity
    {
        /// <summary>
        /// Gets or sets the Database ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
       
    }
}
