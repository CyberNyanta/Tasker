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
    public class TaskListFragment : BaseListFragment
    {
        public enum TaskListType { All, Project, Search}

        private Adapters.TaskListAdapter _taskList;
        private IList<Task> _tasks;
        private IList<Project> _projects;
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
            _projects = _viewModel.GetAllProjects();
            _taskList = new Adapters.TaskListAdapter(this.Activity, _tasks, _projects);

            _listView.Adapter = _taskList;
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
        }
    }
}