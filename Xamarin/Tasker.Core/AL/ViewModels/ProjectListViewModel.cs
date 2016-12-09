using System;
using System.Collections.Generic;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class ProjectListViewModel : BaseViewModel, IProjectListViewModel
    {
        private IProjectManager _projectManager;

        public int Id { get; set; }

        public event Action OnCollectionChanged;

        public ProjectListViewModel(IProjectManager projectManager) : base()
        {
            _projectManager = projectManager;
        }

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

        public Project GetItem(int id)
        {
            return id != 0 ? _projectManager.Get(id) : null;
        }

        public int SaveItem(Project item)
        {
            return _projectManager.SaveItem(item);
        }

        public int DeleteItem(int id)
        {
            return _projectManager.Delete(id);
        }
    }
}
