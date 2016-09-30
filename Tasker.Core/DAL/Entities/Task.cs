using System;
using SQLite;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Entities
{
    public class Task : BaseBusinessEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
       
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime RemindDate { get; set; }

        public int Color { get; set; }
    }
}
