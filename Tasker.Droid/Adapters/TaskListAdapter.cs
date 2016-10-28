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
using Android.Support.V7.App;

namespace Tasker.Droid.Adapters
{
    public class TaskListAdapter : BaseAdapter<Task>
    {
        private Activity _context;
        private List<Task> _tasks;
        private List<Project> _projects;

        public TaskListAdapter(Activity context, List<Task> tasks, List<Project> projects) : base()
        {
            _context = context;
            _tasks = tasks;
            _projects = projects;
            _tasks.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));
            NotifyDataSetChanged();
        }

        public void Remove(int position)
        {
            _tasks.RemoveAt(position);
            NotifyDataSetChanged();
        }

        public void ChangeDataSet(List<Task> tasks)
        {
            _tasks = tasks;
            _tasks.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));
            NotifyDataSetChanged();
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
            return _tasks[position].ID;
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
            var taskDueDate = view.FindViewById<TextView>(Resource.Id.dueDate);
            var taskProject = view.FindViewById<TextView>(Resource.Id.projectName);
                        
            if (item.IsSolved)
            {
                taskTitle.PaintFlags = PaintFlags.StrikeThruText | PaintFlags.AntiAlias | PaintFlags.EmbeddedBitmapText;
                view.Alpha = TaskConstants.COMPLETED_TASK_BACKGROUND_ALPHA;
            }
            else
            {
                taskTitle.PaintFlags = PaintFlags.AntiAlias | PaintFlags.EmbeddedBitmapText;
                view.Alpha = TaskConstants.TASK_BACKGROUND_ALPHA;
            }
                 
            taskTitle.Text = item.Title;
            //Set Task Color
            if (item.Color != TaskColors.None)
            {
                border.SetBackgroundColor(Color.ParseColor(TaskConstants.Colors[item.Color]));
            }
            else
                border.SetBackgroundColor(view.DrawingCacheBackgroundColor);
            //Set Task due date
            
            if (item.DueDate != DateTime.MaxValue)
            {
                taskDueDate.Text = item.DueDate.ToString(_context.GetString(Resource.String.datetime_regex));
            }
            else
            {
                taskDueDate.Text = "";
            }
            
            if (item.ProjectID != 0)
            {
                taskProject.Text = _projects.First((x)=> x.ID==item.ProjectID).Title;
            }
            else
            {
                taskProject.Text = _context.GetString(Resource.String.project_inbox);
            }
                        
            return view;
        }

        public Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }
    }
}