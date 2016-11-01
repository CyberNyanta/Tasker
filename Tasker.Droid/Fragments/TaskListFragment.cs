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
using Java.Util;

using TinyIoC;
using Com.Wdullaer.Swipeactionadapter;
using Com.Github.Jjobes.Slidedatetimepicker;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.AL.Utils;
using Tasker.Core.DAL.Entities;
using Tasker.Droid.Activities;
using Tasker.Core;
using Tasker.Droid.Adapters;

namespace Tasker.Droid.Fragments
{
    public class TaskListFragment : BaseListFragment, SwipeActionAdapter.ISwipeActionListener
    {
        public enum TaskListType { AllOpen, AllSolve, ProjectOpen, ProjectSolve, Today, Tomorrow, NextWeek, }

        private Adapters.TaskListAdapter _taskListAdapter;
        private SwipeActionAdapter _swipeActionAdapter;
        private List<Task> _tasks;
        private List<Project> _projects;
        private ITaskListViewModel _viewModel;
        private TaskListType _taskListType;
        private int _projectId;
        private bool isAllSoved;

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
            if (_taskListType.IsOpenType())
            {
                int id = (int)e.Id;
                Intent intent = new Intent(this.Activity, typeof(TaskEditCreateActivity));
                intent.PutExtra("TaskId", id);
                StartActivity(intent);
            }   
        }

        protected override void FabClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(TaskEditCreateActivity));
            if (_taskListType == TaskListType.ProjectOpen)
            {
                intent.PutExtra("ProjectId", _projectId);
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
            _taskListType = (TaskListType)Activity.Intent.GetIntExtra("TaskListType", (int)TaskListType.AllOpen);
           
            switch (_taskListType)
            {
                case TaskListType.AllOpen:
                    _tasks = _viewModel.GetAllOpen();
                    isAllSoved = _viewModel.GetAllSolve().Count > 0 ? true : false;
                    break;
                case TaskListType.AllSolve:
                    HideFAB();
                    _tasks = _viewModel.GetAllSolve();
                    break;
                case TaskListType.ProjectSolve:
                    HideFAB();
                    _projectId = Activity.Intent.GetIntExtra("ProjectId", 0);
                    _tasks = _viewModel.GetProjectSolveTasks(_projectId);
                    break;
                case TaskListType.ProjectOpen:
                    _projectId = Activity.Intent.GetIntExtra("ProjectId", 0);
                    _tasks = _viewModel.GetProjectOpenTasks(_projectId);
                    isAllSoved = _viewModel.GetProjectSolveTasks(_projectId).Count > 0 ? true : false;
                    break;
                case TaskListType.Today:
                    
                    _tasks = _viewModel.GetForToday();
                    break;
                case TaskListType.Tomorrow:

                    _tasks = _viewModel.GetForTomorrow();
                    break;
                case TaskListType.NextWeek:

                    _tasks = _viewModel.GetForNextWeek();
                    break;
            }

            if (_tasks.Count == 0)
            {
                OnTasksListIsEmpty();
            }
            else
            {
                _listView.Visibility = ViewStates.Visible;
            }            

            _projects = _viewModel.GetAllProjects();

            _taskListAdapter = new Adapters.TaskListAdapter(Activity, _tasks, _projects);
            _swipeActionAdapter = new SwipeActionAdapter(_taskListAdapter);
            _swipeActionAdapter.SetListView(_listView);
            _swipeActionAdapter.SetSwipeActionListener(this);
            // Set the SwipeActionAdapter as the Adapter for ListView
            _listView.Adapter = _swipeActionAdapter;
            // Set backgrounds for the swipe directions
            if (_taskListType.IsOpenType())
            {
                _swipeActionAdapter.AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Layout.task_item_background_unsolve_left)
                               .AddBackground(SwipeDirection.DirectionNormalRight, Resource.Layout.task_item_background_unsolve_right);
            }
            else
            {
                _swipeActionAdapter.AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Layout.task_item_background_solve_left)
                               .AddBackground(SwipeDirection.DirectionNormalRight, Resource.Layout.task_item_background_solve_right);
            }

        }

        private void OnTasksListIsEmpty()
        {
            var image = Activity.FindViewById<ImageView>(Resource.Id.empty_items_image);
            var coment = Activity.FindViewById<TextView>(Resource.Id.empty_items_comment);
            var container = Activity.FindViewById<FrameLayout>(Resource.Id.empty_items_container);
            if (_taskListType.IsSolveType())
            {
                container.Alpha = TaskConstants.COMPLETED_TASK_BACKGROUND_ALPHA;
                image.SetImageDrawable(Activity.GetDrawable(Resource.Drawable.empty_items));
                coment.Text = Activity.GetString(Resource.String.empty_items_comment_no_complete);
            }
            else if(isAllSoved)
            {
                container.Alpha = TaskConstants.TASK_BACKGROUND_ALPHA;
                image.SetImageDrawable(Activity.GetDrawable(Resource.Drawable.empty_items_solve));
                coment.Text = Activity.GetString(Resource.String.empty_items_comment_all_solve);
            }
            else
            {
                container.Alpha = TaskConstants.TASK_BACKGROUND_ALPHA;
                image.SetImageDrawable(Activity.GetDrawable(Resource.Drawable.empty_items));
                coment.Text = Activity.GetString(Resource.String.empty_items_comment_not_added);
            }

            _listView.Visibility = ViewStates.Gone;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_show_solve_tasks:
                    Intent intent = new Intent(this.Activity, typeof(CompleteTaskListActivity));
                    intent.PutExtra("TaskListType", (int)(_taskListType == TaskListType.ProjectOpen ? TaskListType.ProjectSolve : TaskListType.AllSolve));
                    intent.PutExtra("ProjectId", _projectId);
                    StartActivity(intent);
                    break;

                case Resource.Id.menu_search:

                    var intent2 = new Intent(Activity, typeof(SearchTaskListActivity));
                    intent2.PutExtra("IsProjectTaskSearch", (_taskListType == TaskListType.ProjectOpen ? true : false));
                    intent2.PutExtra("ProjectId", _projectId);
                    StartActivity(intent2);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void SolveTask()
        {
            var task = _viewModel.GetItem(_viewModel.Id);
            if (task.IsSolved)
            {
                _viewModel.ChangeStatus(task);
            }
            else
            {
                _viewModel.ChangeStatus(task);
            }
        }

        private void DeleteTask(Action callback)
        {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            alert.SetTitle(GetString(Resource.String.confirm_delete_task));
            alert.SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) =>
            {
                callback?.Invoke();

            });
            alert.SetCancelable(true);
            alert.SetNegativeButton(GetString(Resource.String.dialog_cancel), (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void SetDueDate(Task task,int position)
        {
            AlertDialog dialog = null;
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            dialog = builder.SetCancelable(true)
                            .SetAdapter(new DueDateListAdapter(Activity, task.DueDate, (sender, args) =>
                            {
                                dialog.Dismiss();
                                var selected = (TaskDueDates)(int)((View)sender).Tag;
                                switch (selected)
                                {
                                    case TaskDueDates.Today:
                                        task.DueDate = DateTime.Today;                                       
                                        break;
                                    case TaskDueDates.Tomorrow:
                                        task.DueDate = DateTime.Today.AddDays(1);
                                        break;
                                    case TaskDueDates.NextWeek:
                                        task.DueDate = DateTime.Today.AddDays(7);
                                        break;
                                    case TaskDueDates.Remove:
                                        task.DueDate = DateTime.MaxValue;
                                        break;
                                    case TaskDueDates.PickDataTime:
                                        var dateTimePicker = new SlideDateTimePicker.Builder(Activity.SupportFragmentManager);
                                        var pickerDialog = dateTimePicker.SetInitialDate(new Date())
                                                                   .SetMinDate(new Date())
                                                                   .SetListener(new DueDateListener(task, () => 
                                                                   {
                                                                       _viewModel.SaveItem(task);
                                                                       _taskListAdapter.SaveChanges(task, position);
                                                                   }))
                                                                   .SetTheme(0)
                                                                   .Build();
                                        pickerDialog.Show();
                                        break;
                                }
                                _viewModel.SaveItem(task);
                                _taskListAdapter.SaveChanges(task, position);
                            }), default(IDialogInterfaceOnClickListener))
                            .Show();


        }

        public class DueDateListener : SlideDateTimeListener
        {
            Task _task;
            event Action _callback;
            public DueDateListener(Task task, Action callback) : base()
            {
                _task = task;
                _callback += callback;
            }
            public override void OnDateTimeSet(Date p0)
            {
                _task.DueDate = p0.Time.UnixTimeToDateTime();
                _callback?.Invoke();
            }
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
                _viewModel.Id = (int)_taskListAdapter.GetItemId(position);
                if (direction.IsRight)
                {                    
                    SolveTask();
                    _taskListAdapter.Remove(position);
                    isAllSoved = true;
                    _swipeActionAdapter.NotifyDataSetChanged();
                    if (_tasks.Count == 0)
                        OnTasksListIsEmpty();
                }                
                else if (_taskListType.IsSolveType())
                {
                    DeleteTask(() =>
                    {
                        _viewModel.DeleteItem(_viewModel.Id);
                        _taskListAdapter.Remove(position);
                        isAllSoved = false;
                        _swipeActionAdapter.NotifyDataSetChanged();
                        if (_tasks.Count == 0)
                            OnTasksListIsEmpty();
                    });
                }
                else
                {
                    
                    SetDueDate(_viewModel.GetItem(_viewModel.Id), position);
                }

             
            }
        }

        public bool ShouldDismiss(int position, SwipeDirection direction)
        {           
            return direction.IsRight ? true : false;
        }
        #endregion
    }
    public static class Extensions
    {
        public static bool IsAllType(this TaskListFragment.TaskListType type)
        {
            return type == TaskListFragment.TaskListType.AllOpen || type == TaskListFragment.TaskListType.AllSolve;
        }

        public static bool IsProjectType(this TaskListFragment.TaskListType type)
        {
            return type == TaskListFragment.TaskListType.ProjectOpen || type == TaskListFragment.TaskListType.ProjectSolve;
        }

        public static bool IsOpenType(this TaskListFragment.TaskListType type)
        {
            return type == TaskListFragment.TaskListType.AllOpen || type == TaskListFragment.TaskListType.ProjectOpen;
        }
        public static bool IsSolveType(this TaskListFragment.TaskListType type)
        {
            return type == TaskListFragment.TaskListType.AllSolve || type == TaskListFragment.TaskListType.ProjectSolve;
        }
    }
}