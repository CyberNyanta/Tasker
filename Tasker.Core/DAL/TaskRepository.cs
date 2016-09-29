
using Tasker.Core.DL.Entities;

namespace Tasker.Core.DAL
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(string dbLocation) : base(dbLocation)
        {
        }
    }
}
