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


            String[] content = new String[20];
            for (int i = 0; i < 20; i++) content[i] = "Row " + (i + 1);
            ArrayAdapter<String> stringAdapter = new ArrayAdapter<String>(
                    this.Activity,
                    Resource.Layout.row_bg,
                    Resource.Id.text,
                    content
            );
            var mAdapter = new SwipeActionAdapter(_listAdapter);

            // Pass a reference of your ListView to the SwipeActionAdapter
            mAdapter.SetListView(_listView);
            mAdapter.SetSwipeActionListener( new ActionListener(mAdapter));
            // Set the SwipeActionAdapter as the Adapter for your ListView

            // Set backgrounds for the swipe directions



            _listView.Adapter = mAdapter;

            mAdapter.AddBackground(SwipeDirection.DirectionFarLeft, Resource.Layout.row_bg_left_far)
            .AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Layout.row_bg_left)
            .AddBackground(SwipeDirection.DirectionFarRight, Resource.Layout.row_bg_right_far)
            .AddBackground(SwipeDirection.DirectionNormalRight, Resource.Layout.row_bg_right);
        }

        public class ActionListener : Java.Lang.Object, SwipeActionAdapter.ISwipeActionListener
        {
            SwipeActionAdapter _adapter;
            public ActionListener(SwipeActionAdapter adapter)
            {
                _adapter = adapter;
            }
            public bool HasActions(int position, SwipeDirection direction)
            {
                if (direction.IsLeft) return true; // Change this to false to disable left swipes
                if (direction.IsRight) return true;
                return false;
            }

            public void OnSwipe(int[] positionList, SwipeDirection[] directionList)
            {
                for (int i = 0; i < positionList.Length; i++)
                {
                    SwipeDirection direction = directionList[i];
                    int position = positionList[i];
                   
                    _adapter.NotifyDataSetChanged();
                }
            }

            public bool ShouldDismiss(int position, SwipeDirection direction)
            {
                return direction == SwipeDirection.DirectionNormalLeft;
            }
        }

    }
}