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
using Android.Graphics;

using Tasker.Core.DAL.Entities;
using Tasker.Core;

namespace Tasker.Droid.Adapters
{
    public class TaskListAdapter : BaseAdapter<Task>
    {
        private Activity context;
        private IList<Task> tasks;

        public TaskListAdapter(Activity context, IList<Task> tasks) : base()
        {
            this.context = context;
            this.tasks = tasks;
        }

        public override Task this[int position]
        {
            get { return tasks[position]; }
        }

        public override int Count
        {
            get { return tasks.Count; }
        }

        public override long GetItemId(int position)
        {
            return tasks[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            var item = tasks[position];
            View view;

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            if (convertView == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.tasklistitem, null);
            }
            else
            {
                view = convertView;
            }
            

            var taskTitle = view.FindViewById<TextView>(Resource.Id.taskTitle);
            taskTitle.Text = item.Title;

            if (item.Color != TaskColors.None)
            {
                taskTitle.SetBackgroundColor(Color.ParseColor(TaskConstants.Colors[(int)item.Color]));
            }
            else
                taskTitle.SetBackgroundColor(parent.DrawingCacheBackgroundColor);

            var taskDueDate = view.FindViewById<TextView>(Resource.Id.dueDate);
            taskDueDate.Text = item.DueDate.ToString();
            var project = view.FindViewById<TextView>(Resource.Id.projectName);
            project.Text = "project";

            //Finally return the view
            return view;
        }
    }
}