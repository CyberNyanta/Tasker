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
using Tasker.Core.BL.Contracts;

namespace Tasker.Droid.Adapters
{
    public class TaskListAdapter : BaseAdapter<Task>
    {
        private Activity _context;
        private IList<Task> _tasks;
        private IList<Project> _projects;

        public TaskListAdapter(Activity context, IList<Task> tasks, IList<Project> projects) : base()
        {
            _context = context;
            _tasks = tasks;
            _projects = projects;
        }

        public override Task this[int position]
        {
            get { return _tasks[position]; }
        }

        public override int Count
        {
            get { return _tasks.Count; }
        }

        public override long GetItemId(int position)
        {
            return _tasks[position-1].ID;
        }       

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            var item = _tasks[position];
            View view;
            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            if (convertView == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.task_list_item, null);
            }
            else
            {
                view = convertView;
            }
            
            var taskTitle = view.FindViewById<TextView>(Resource.Id.taskTitle);
            var border = view.FindViewById<View>(Resource.Id.task_color_border);

            taskTitle.Text = item.Title;
            //Set Task Color
            if (item.Color != TaskColors.None)
            {
                border.SetBackgroundColor(Color.ParseColor(TaskConstants.Colors[(int)item.Color]));
            }
            else
                border.SetBackgroundColor(parent.DrawingCacheBackgroundColor);
            //Set Task due date
            var taskDueDate = view.FindViewById<TextView>(Resource.Id.dueDate);
            if (item.DueDate != DateTime.MinValue)
            {
                taskDueDate.Text = item.DueDate.ToString(_context.GetString(Resource.String.datetime_regex));
            }
            else
            {
                taskDueDate.Text = _context.GetString(Resource.String.datetime_none);
            }
            //Set Task project
            var taskProject = view.FindViewById<TextView>(Resource.Id.projectName);
            if (item.ProjectID != 0)
            {
                taskProject.Text = _projects.FirstOrDefault((x)=> x.ID==item.ProjectID).Title;
            }
            else
            {
                taskProject.Text = _context.GetString(Resource.String.project_inbox);
            }
            

            //Finally return the view
            return view;
        }
    }
}