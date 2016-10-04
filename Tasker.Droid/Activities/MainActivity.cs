using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TinyIoC;
using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Entities;
using System.Collections.Generic;

namespace Tasker.Droid
{
	[Activity (Label = "Tasker", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Tasker")]
	public class MainActivity : Activity
	{
        protected Adapters.TaskListAdapter taskList;
        protected IList<Task> tasks;
        protected ListView taskListView = null;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


            taskListView = FindViewById<ListView>(Resource.Id.taskList);
            //var taskManager = TinyIoCContainer.Current.Resolve<TaskManager>();
            //taskManager.SaveItem(new Task { Title = "asd", Description = "fgjh" });
            //var r = taskManager.GetAll();
            ////var ut = new Core.DAL.UnitOfWork(new Utils.DatabasePath());
            ////var taskManager = new TaskManager(ut);
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
    }
}


