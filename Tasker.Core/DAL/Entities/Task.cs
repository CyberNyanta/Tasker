using System;

//using SQLite;

using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Entities
{
    //[Table("Task")]
    public class Task : BaseBusinessEntity
    {
        public Task()
        {
            DueDate = DateTime.MaxValue;
            RemindDate = DateTime.MaxValue;
        }
//[MaxLength(TaskConstants.TASK_TITLE_MAX_LENGTH)]
        public string Title { get; set; }

        //[MaxLength(TaskConstants.TASK_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }
       
        public int ProjectID { get; set; }

        public DateTime DueDate { get; set; }
        
        public DateTime RemindDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public bool IsCompleted { get; set; }

        public TaskColors Color { get; set; }
    }
}
