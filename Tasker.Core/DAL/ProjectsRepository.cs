using Tasker.Core.DL;
using Tasker.Core.DL.Entities;

namespace Tasker.Core.DAL
{
    public class ProjectsRepository : BaseRepository<Project>
    {
        public ProjectsRepository(TaskDatabase db) : base(db)
        {
        }
    }
}
