//using SQLite;

using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Entities
{
   // [Table("Project")]
    public class Project : BaseBusinessEntity
    {
        //[MaxLength(TaskConstants.PROJECT_TITLE_MAX_LENGTH)]
        public string Title { get; set; }

        public int CountOfOpenTasks { get; set; }

        public int CountOfCompletedTasks { get; set; }
    }
}
