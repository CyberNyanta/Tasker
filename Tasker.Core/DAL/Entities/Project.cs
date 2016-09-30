
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.DAL.Entities
{
    public class Project : BaseBusinessEntity
    {
        public string Title { get; set; }
    }
}
