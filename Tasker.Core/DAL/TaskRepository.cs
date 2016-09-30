
using Tasker.Core.DL;
using Tasker.Core.DL.Entities;

namespace Tasker.Core.DAL
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(TaskDatabase db) : base(db)
        {
        }
    }
}
