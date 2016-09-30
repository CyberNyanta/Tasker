
using Tasker.Core.DL;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.DAL.Repositories
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(TaskerDatabase db) : base(db)
        {
        }
    }
}
