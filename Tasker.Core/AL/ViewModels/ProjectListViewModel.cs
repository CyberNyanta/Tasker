using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class ProjectListViewModel : BaseViewModel, IProjectListViewModel
    {
        private IProjectManager _projectManager;

        public ProjectListViewModel(IProjectManager projectManager) : base()
        {
            _projectManager = projectManager;
        }

        public event Action OnCollectionChanged;

        public void DeleteGroup(IList<Project> group)
        {
            _projectManager.DeleteGroup(group);
        }

        public List<Project> GetAll()
        {
            return _projectManager.GetAll();
        }

        protected virtual void RaiseOnCollectionChanged()
        {
            OnCollectionChanged?.Invoke();
        }
    }
}
