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

namespace Tasker.Droid
{
	[Activity (Label = "Tasker.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};

            var taskManager = TinyIoCContainer.Current.Resolve<TaskManager>();
            taskManager.SaveItem(new Task { Title = "asd", Description = "fgjh" });
            var r = taskManager.GetAll();
            //var ut = new Core.DAL.UnitOfWork(new Utils.DatabasePath());
            //var taskManager = new TaskManager(ut);
        }
	}
}


