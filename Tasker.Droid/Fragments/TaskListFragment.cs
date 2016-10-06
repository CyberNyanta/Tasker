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

namespace Tasker.Droid.Fragments
{
    public class TaskListFragment : Fragment
    {
        private Adapters.TaskListAdapter _taskList;
        private IList<Task> _tasks;
        private ListView _taskListView = null;
        private ITaskListViewModel _viewModel;
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
            _viewModel = TinyIoCContainer.Current.Resolve<ITaskListViewModel>();
            _taskListView = view.FindViewById<ListView>(Resource.Id.taskList);
            _fab = view.FindViewById<FAB>(Resource.Id.fab);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _fab.Show(false);
            _fab.PostDelayed(ShowFab, 300);
        }

        public override void OnPause()
        {
            base.OnPause();
            _taskListView.Scroll -= ListView_Scroll;
        }

        public override void OnResume()
        {
            base.OnResume();

            _tasks = new List<Task>
            {
                new Task { Title = "TaskTitle", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle1", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle2", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle3", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle4", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle5", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle6", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle7", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle",  DueDate = DateTime.Now },
                new Task { Title = "TaskTitle1", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle2", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle3", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle4", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle5", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle6", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle7", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle1", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle2", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle3", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle4", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle5", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle6", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle7", DueDate = DateTime.Now }
            };

            _taskList = new Adapters.TaskListAdapter(this.Activity, _tasks);
            _taskListView.Scroll += ListView_Scroll;
            _taskListView.Adapter = _taskList;
        }

        private void ShowFab()
        {
            _fab.Show(true);
            _fab.SetShowAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.show_from_bottom));
            _fab.SetHideAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.hide_to_bottom));
        }

        private void ListView_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {

            if (e.FirstVisibleItem > previousVisibleItem)
            {
                _fab.Hide(true);
            }
            else if (e.FirstVisibleItem < previousVisibleItem)
            {
                _fab.Show(true);
            }
            previousVisibleItem = e.FirstVisibleItem;
        }
    }
}