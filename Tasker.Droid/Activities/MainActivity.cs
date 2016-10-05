using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;

using Java.Util;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using FAB = Clans.Fab.FloatingActionButton;

using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Entities;
using Tasker.Core.AL.Utils;
using Tasker.Core.AL.ViewModels.Contracts;
using Android.Support.V7.App;
using Android.Views.Animations;

namespace Tasker.Droid
{


    [Activity (Label = "Tasker", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Tasker")]

    public class MainActivity : AppCompatActivity
    {
        private Adapters.TaskListAdapter _taskList;
        private IList<Task> _tasks;
        private ListView _taskListView = null;
        private ITaskListViewModel _viewModel;
        private FAB _fab;
        private readonly bool hideFab;
        private int previousVisibleItem;

        protected override void OnCreate (Bundle bundle)
		{
            
            base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.main);

            _viewModel = TinyIoCContainer.Current.Resolve<ITaskListViewModel>();
                                    
            _taskListView = FindViewById<ListView>(Resource.Id.taskList);
            _fab = FindViewById<FAB>(Resource.Id.fab);
            _fab.Show(false);
            _fab.PostDelayed(ShowFab, 300);
        }


        protected override void OnResume()
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
                new Task { Title = "TaskTitle", DueDate = DateTime.Now },
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

            // create our adapter
            _taskList = new Adapters.TaskListAdapter(this, _tasks);

            _taskListView.Scroll += ListView_Scroll;

            //Hook up our adapter to our ListView
            _taskListView.Adapter = _taskList;

        }

        protected override void OnPause()
        {
            base.OnPause();
            _taskListView.Scroll -= ListView_Scroll;
            ;
        }

        private void ShowFab()
        {
            _fab.Show(true);
            _fab.SetShowAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.show_from_bottom));
            _fab.SetHideAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.hide_to_bottom));
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


