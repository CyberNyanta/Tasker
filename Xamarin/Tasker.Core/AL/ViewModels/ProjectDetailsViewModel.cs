using System;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.AL.ViewModels
{
    public class ProjectDetailsViewModel : BaseViewModel, IProjectDetailsViewModel
    {
        private IProjectManager _projectManager;

        public int Id { get; set; }

        public ProjectDetailsViewModel(IProjectManager projectManager) : base()
        {
            _projectManager = projectManager;
        }

        public Project GetItem()
        {
            return Id != 0 ? _projectManager.Get(Id) : null;
        }

        public int SaveItem(Project item)
        {
            return _projectManager.SaveItem(item);
        }

        public int DeleteItem()
        {
            return _projectManager.Delete(Id);
        }

        public Project GetItem(int id)
        {
            return _projectManager.Get(id);
        }

        public int DeleteItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
