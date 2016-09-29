using Tasker.Core.DL.Entities;

namespace Tasker.Core.DAL
{
    public class ProjectsRepository : BaseRepository<Project>
    {
        public ProjectsRepository(string dbLocation) : base(dbLocation)
        {
        }
    }
}
