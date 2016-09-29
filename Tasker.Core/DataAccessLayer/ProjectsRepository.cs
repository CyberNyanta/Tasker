using Tasker.Core.DataLayer.Entities;

namespace Tasker.Core.DataAccessLayer
{
    public class ProjectsRepository : BaseRepository<Project>
    {
        public ProjectsRepository(string dbLocation) : base(dbLocation)
        {
        }
    }
}
