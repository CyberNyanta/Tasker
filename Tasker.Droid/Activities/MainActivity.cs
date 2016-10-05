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

using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Entities;
using Tasker.Core.AL.Utils;







namespace Tasker.Droid
{


    [Activity (Label = "Tasker", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Tasker")]
	public class MainActivity : FragmentActivity
    {
        protected Adapters.TaskListAdapter taskList;
        protected IList<Task> tasks;
        protected ListView taskListView = null;

        protected override void OnCreate (Bundle bundle)
		{

            base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.main);

            taskListView = FindViewById<ListView>(Resource.Id.taskList);

            var javaTime = new Java.Util.Date();
            var cTime = DateTime.UtcNow;
            var r = cTime.ToUnixTime();

            var b = new SlideDateTimePicker.Builder(SupportFragmentManager);
            b.SetInitialDate(javaTime);

            b.SetListener(new CustomSlideDateTimeListener(this));
            b.SetTheme(1);
            var p = b.Build();
            p.Show();

            if (taskListView != null)
            {
                taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                    
                };
            }

        }


        protected override void OnResume()
        {
            base.OnResume();

            tasks = new List<Task>
            {
                new Task { Title = "TaskTitle", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle1", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle2", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle3", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle4", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle5", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle6", DueDate = DateTime.Now },
                new Task { Title = "TaskTitle7", DueDate = DateTime.Now },
            };

            // create our adapter
            taskList = new Adapters.TaskListAdapter(this, tasks);

            
            //Hook up our adapter to our ListView
            taskListView.Adapter = taskList;

        }

        public class CustomSlideDateTimeListener : SlideDateTimeListener
        {
            MainActivity _activity;
            public CustomSlideDateTimeListener(MainActivity activity)
            {
                _activity = activity;
            }
            public override void OnDateTimeSet(Date p0)
            {
                Toast.MakeText(_activity, p0.ToString(), ToastLength.Long).Show();
            }

            public void onDateTimeCancel()
            {
                Toast.MakeText(_activity,"Canceled", ToastLength.Long).Show();
            }
        }
    }

    
}


