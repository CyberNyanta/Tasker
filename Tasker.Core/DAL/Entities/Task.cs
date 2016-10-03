using System;
using SQLite;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Entities
{
    [Table("Task")]
    public class Task : BaseBusinessEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
       
        public int ProjectID { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime RemindDate { get; set; }
        
        public bool IsSolved { get; set; }

        public int Color { get; set; }
    }
}
