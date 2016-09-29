
using Tasker.Core.DataLayer.Entities;

namespace Tasker.Core.DataAccessLayer
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(string dbLocation) : base(dbLocation)
        {
        }
    }
}
