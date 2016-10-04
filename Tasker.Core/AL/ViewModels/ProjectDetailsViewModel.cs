using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class ProjectDetailsViewModel : BaseViewModel, IProjectDetailsViewModel
    {
        private IProjectManager _projectManager;   
        private Project current;
       
        public ProjectDetailsViewModel(IProjectManager projectManager) : base()
        {
            _projectManager = projectManager;
        }

        public int Id { get; set; }

        public Project GetItem()
        {
            if (Id != 0)
            {
                return _projectManager.Get(Id);
            }
            else
            {
                return null;
            }
        }

        public int SaveItem(Project item)
        {
            return _projectManager.SaveItem(item);
        }

    }
}
