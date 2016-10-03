using System;
using SQLite;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.Constants;

namespace Tasker.Core.DAL.Entities
{
    [Table("Task")]
    public class Task : BaseBusinessEntity
    {
        [MaxLength(TextConstant.TASK_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [MaxLength(TextConstant.TASK_TITLE_MAX_LENGTH)]
        public string Description { get; set; }
       
        public int ProjectID { get; set; }

        public DateTime DueDate { get; set; }
        
        public DateTime RemindDate { get; set; }
        
        public bool IsSolved { get; set; }

        public int Color { get; set; }
    }
}
