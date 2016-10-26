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
using Tasker.Core.AL.Utils;
using Tasker.Core;
using Tasker.Droid.Activities;

namespace Tasker.Droid.Fragments
{
    public class ProjectListFragment : BaseListFragment, SwipeActionAdapter.ISwipeActionListener
    {
        private Adapters.ProjectListAdapter _listAdapter;
        private SwipeActionAdapter _swipeActionAdapter;
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
            Intent intent = new Intent(this.Activity, typeof(ProjectTasksListActivity));
            intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.ProjectOpen);
            intent.PutExtra("ProjectId", id);
            StartActivity(intent);
        }

        protected override void FabClick(object sender, EventArgs e)
        {
            View view = Activity.LayoutInflater.Inflate(Resource.Layout.project_edit_create_dialog, null);
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            alert.SetTitle(GetString(Resource.String.project_create_dialog))
                 .SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) =>
                        {
                            var projectTitle = view.FindViewById<EditText>(Resource.Id.project_dialog_title);
                            if (projectTitle.Text.IsLengthInRange(TaskConstants.PROJECT_TITLE_MAX_LENGTH, 1))
                            {
                                var project = new Project { Title = projectTitle.Text };
                                _viewModel.SaveItem(project);
                                _listAdapter.Add(project);

                            }

                        })            
                 .SetView(view)
                 .SetCancelable(true)
                 .SetNegativeButton(GetString(Resource.String.dialog_cancel), (senderAlert, args) => { })
                 .Show();
        }


        public override void OnResume()
        {
            base.OnResume();
            _tasks = _taskViewModel.GetAll();
            _projects = _viewModel.GetAll();
            _listAdapter = new Adapters.ProjectListAdapter(this.Activity, _tasks, _projects);


            _swipeActionAdapter = new SwipeActionAdapter(_listAdapter);
            _swipeActionAdapter.SetListView(_listView);
            _swipeActionAdapter.SetSwipeActionListener(this);
            _listView.Adapter = _swipeActionAdapter;

            _swipeActionAdapter.AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Layout.project_item_background_left)
                               .AddBackground(SwipeDirection.DirectionNormalRight, Resource.Layout.project_item_background_right);
        }


        private void DeleteProject(Action callback)
        {

            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            alert.SetTitle(GetString(Resource.String.confirm_delete_project));
            alert.SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) =>
            {
                callback?.Invoke();
            });
            alert.SetCancelable(true);
            alert.SetNegativeButton(GetString(Resource.String.dialog_cancel), (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void EditProject(int position)
        {
         
            EditText projectTitle = null;
            var project = _viewModel.GetItem(_viewModel.Id);
            View view = Activity.LayoutInflater.Inflate(Resource.Layout.project_edit_create_dialog, null);
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            alert.SetTitle(GetString(Resource.String.project_edit_dialog))
                 .SetView(view)
                 .SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) =>
                        {

                            if (projectTitle.Text.IsLengthInRange(TaskConstants.PROJECT_TITLE_MAX_LENGTH, 1))
                            {
                                project.Title = projectTitle.Text;
                                _viewModel.SaveItem(project);

                                _listAdapter.Save(project, position);                    
                                _swipeActionAdapter.NotifyDataSetChanged();
                            }

                        })            
                .SetCancelable(true)
                .SetNegativeButton(GetString(Resource.String.dialog_cancel), (senderAlert, args) => { })
                .Show();

            projectTitle = view.FindViewById<EditText>(Resource.Id.project_dialog_title);      
            projectTitle.Text = project.Title;
        }

        #region  SwipeActionAdapter.ISwipeActionListener
        public bool HasActions(int position, SwipeDirection direction)
        {
            if (position == 0) return false;
            if (direction.IsLeft) return true;
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
                    DeleteProject(() =>
                    {
                        _viewModel.DeleteItem(_viewModel.Id);
                        _listAdapter.Remove(position);
                        _swipeActionAdapter.NotifyDataSetChanged();
                    });                  
                }
                else
                {
                    EditProject(position);
                }               
            }
        }

        public bool ShouldDismiss(int position, SwipeDirection direction)
        {
            _viewModel.Id = (int)_listAdapter.GetItemId(position);
            return false;
        }
        #endregion
    }
}