using Tasker.Core.DL;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.DAL.Repositories
{
    public class ProjectsRepository : BaseRepository<Project>
    {
        public ProjectsRepository(TaskerDatabase db) : base(db)
        {
        }
    }
}
