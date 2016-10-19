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
using Android.Views.Animations;

using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;
using FAB = Clans.Fab.FloatingActionButton;

using Tasker.Core.AL.ViewModels.Contracts;

using Tasker.Core.DAL.Entities;
using Tasker.Droid.Activities;
using Com.Wdullaer.Swipeactionadapter;

namespace Tasker.Droid.Fragments
{
    public class TaskListFragment : BaseListFragment, SwipeActionAdapter.ISwipeActionListener
    {
        public enum TaskListType { All, Project, Search }

        private Adapters.TaskListAdapter _taskListAdapter;
        private SwipeActionAdapter _swipeActionAdapter;
        private List<Task> _tasks;
        private List<Project> _projects;
        private ITaskListViewModel _viewModel;
        private TaskListType _taskListType;
        private int _projectId;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.task_list, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _viewModel = TinyIoCContainer.Current.Resolve<ITaskListViewModel>();
            _listView = view.FindViewById<ListView>(Resource.Id.taskList);
            _listView.ItemClick += ItemClick;
            HasOptionsMenu = true;

        }

        private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int id = (int)e.Id;
            Intent intent = new Intent(this.Activity, typeof(TaskDetailsActivity));
            intent.PutExtra("TaskId", id);
            StartActivityForResult(intent, (int)Result.Ok);
        }

        protected override void FabClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(TaskEditCreateActivity));
            if (_taskListType == TaskListType.Project)
            {
                intent.PutExtra("ProjectId", 0);
            }
            StartActivity(intent);
        }


        public override void OnResume()
        {
            base.OnResume();

            TaskInitialization();


        }

        private void TaskInitialization()
        {
            _taskListType = (TaskListType)Activity.Intent.GetIntExtra("TaskListType", (int)TaskListType.All);
            switch (_taskListType)
            {
                case TaskListType.All:
                    _tasks = _viewModel.GetAll();
                    break;
                case TaskListType.Project:
                    _projectId = Activity.Intent.GetIntExtra("ProjectId", 0);
                    _tasks = _viewModel.GetProjectTasks(_projectId);
                    break;
                case TaskListType.Search:
                    throw new NotImplementedException();
                    break;
            }
            var taskWithMinDate = _tasks.FindAll(t => t.DueDate == DateTime.MinValue);
            _tasks = _tasks.FindAll(t => t.DueDate != DateTime.MinValue);
            _tasks.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));


            _tasks.InsertRange(_tasks.Count, taskWithMinDate);
            _projects = _viewModel.GetAllProjects();

            _taskListAdapter = new Adapters.TaskListAdapter(Activity, _tasks, _projects);
            _swipeActionAdapter = new SwipeActionAdapter(_taskListAdapter);
            _swipeActionAdapter.SetListView(_listView);
            _swipeActionAdapter.SetSwipeActionListener(this);
            // Set the SwipeActionAdapter as the Adapter for ListView
            _listView.Adapter = _swipeActionAdapter;
            // Set backgrounds for the swipe directions
            _swipeActionAdapter.AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Layout.row_bg_left)
                               .AddBackground(SwipeDirection.DirectionNormalRight, Resource.Layout.row_bg_right);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_show_hide_solve_tasks:
                   
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        #region  SwipeActionAdapter.ISwipeActionListener
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
                if (direction.IsRight)
                {
                    _taskListAdapter.Remove(position);
                    _swipeActionAdapter.NotifyDataSetChanged();
                }               
            }
        }

        public bool ShouldDismiss(int position, SwipeDirection direction)
        {
            return direction.IsRight ? true : false;
        }
        #endregion
    }
}