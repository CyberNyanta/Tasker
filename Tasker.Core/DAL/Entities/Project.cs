using SQLite;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.Constants;

namespace Tasker.Core.DAL.Entities
{
    [Table("Project")]
    public class Project : BaseBusinessEntity
    {
        [MaxLength(TextConstant.PROJECT_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
    }
}
