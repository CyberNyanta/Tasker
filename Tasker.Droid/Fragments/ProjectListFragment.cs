using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;
using FAB = Clans.Fab.FloatingActionButton;

using Tasker.Core.AL.ViewModels.Contracts;
using Android.Views.Animations;
using Tasker.Core.DAL.Entities;
using Tasker.Droid.Activities;

namespace Tasker.Droid.Fragments
{
    public class ProjectListFragment : BaseListFragment
    {
        private Adapters.TaskListAdapter _taskList;
        private IList<Task> _tasks;
        private IList<Project> _projects;
        private IProjectListViewModel _viewModel;
        private FAB _fab;
        private readonly bool hideFab;
        private int previousVisibleItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.task_list, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _viewModel = TinyIoCContainer.Current.Resolve<IProjectListViewModel>();
            _listView = view.FindViewById<ListView>(Resource.Id.taskList);
            _fab = view.FindViewById<FAB>(Resource.Id.fab);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _listView.ItemClick += ItemClick;
        }

        private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int id = (int)e.Id;
            Intent intent = new Intent(this.Activity, typeof(MainActivity));
            intent.PutExtra("TaskId", id);
            StartActivityForResult(intent, (int)Result.Ok);
        }

        protected override void FabClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(TaskEditCreateActivity));
            intent.PutExtra("TaskId", 0);
            StartActivityForResult(intent, (int)Result.Ok);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == resultCode)
            {
                _projects = _viewModel.GetAll();//TODO Update list after adding
            }
        }

        public override void OnResume()
        {
            base.OnResume();

            _projects = _viewModel.GetAll();
            _taskList = new Adapters.TaskListAdapter(this.Activity, _tasks, _projects);
            _listView.Adapter = _taskList;
        }



    }
}