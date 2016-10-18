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
using Com.Wdullaer.Swipeactionadapter;

using Tasker.Core.AL.ViewModels.Contracts;
using Android.Views.Animations;
using Tasker.Core.DAL.Entities;
using Tasker.Droid.Activities;

namespace Tasker.Droid.Fragments
{
    public class ProjectListFragment : BaseListFragment
    {
        private Adapters.ProjectListAdapter _listAdapter;
        private List<Task> _tasks;
        private List<Project> _projects;
        private IProjectListViewModel _viewModel;
        private ITaskListViewModel _taskViewModel;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.task_list, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _viewModel = TinyIoCContainer.Current.Resolve<IProjectListViewModel>();
            _taskViewModel = TinyIoCContainer.Current.Resolve<ITaskListViewModel>();
            _listView = view.FindViewById<ListView>(Resource.Id.taskList);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _listView.ItemClick += ItemClick;
        }

        private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int id = (int)e.Id;
            Intent intent = new Intent(this.Activity, typeof(ProjectTasksList));
            intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.Project);
            intent.PutExtra("ProjectId", id);
            StartActivity(intent);
        }

        protected override void FabClick(object sender, EventArgs e)
        {
            //TODO DIALOGS
        }


        public override void OnResume()
        {
            base.OnResume();
            _tasks = _taskViewModel.GetAll();
            _projects = _viewModel.GetAll();
            _listAdapter = new Adapters.ProjectListAdapter(this.Activity, _tasks, _projects);

            var mAdapter = new SwipeActionAdapter(_listAdapter);

            // Pass a reference of your ListView to the SwipeActionAdapter
            _listView.Adapter = mAdapter;
            
        }
    }
}