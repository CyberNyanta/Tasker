
using SQLite;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Entities
{
    [Table("Project")]
    public class Project : BaseBusinessEntity
    {
        public string Title { get; set; }
    }
}
