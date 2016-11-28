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
using Android.Support.V7.App;

using Tasker.Core.DAL.Entities;
using Tasker.Core;
using Tasker.Core.BL.Contracts;
using Tasker.Droid.AL.Utils;
using Android.Support.V4.Content;
using Android.Content.Res;

namespace Tasker.Droid.Adapters
{
    public class TaskListAdapter : BaseAdapter<Task>
    {
        private List<Project> _projects;    
        protected Activity Context { get; set; }
        protected List<Task> TaskList { get; set; }

        public TaskListAdapter(Activity context, List<Task> tasks, List<Project> projects) : base()
        {
            Context = context;
            TaskList = tasks;
            _projects = projects;
            //TaskList.Insert(0, null);
            TaskList.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));

            // NotifyDataSetChanged();
        }

        public void Remove(int position)
        {
            TaskList.RemoveAt(position);
            NotifyDataSetChanged();
        }

        public void SaveChanges(Task task, int position) //For date change
        {
            TaskList[position] = task;
            TaskList.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));
            NotifyDataSetChanged();
        }

        public void ChangeDataSet(List<Task> tasks) //For task search
        {
            TaskList = tasks;
            TaskList.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));
            NotifyDataSetChanged();
        }

        public override Task this[int position]
        {
            get { return TaskList[position]; }
        }

        public override int Count
        {
            get { return TaskList.Count; }
        }

        public override long GetItemId(int position)
        {
            return TaskList[position].ID;
        }       
     
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            var item = TaskList[position];
            View view;
            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            if (convertView == null)
            {
                view = Context.LayoutInflater.Inflate(Resource.Layout.task_list_item, null);
            }
            else
            {
                view = convertView;
            }

            var taskTitle = view.FindViewById<TextView>(Resource.Id.taskTitle);
            var border = view.FindViewById<View>(Resource.Id.task_color_border);
            var taskDueDate = view.FindViewById<TextView>(Resource.Id.dueDate);
            var taskProject = view.FindViewById<TextView>(Resource.Id.projectName);
                        
            if (item.IsCompleted)
            {
                taskTitle.PaintFlags = PaintFlags.StrikeThruText | PaintFlags.AntiAlias | PaintFlags.EmbeddedBitmapText;
                view.Alpha = TaskConstants.COMPLETED_TASK_BACKGROUND_ALPHA;
            }
            else
            {
                taskTitle.PaintFlags = PaintFlags.AntiAlias | PaintFlags.EmbeddedBitmapText;
                view.Alpha = TaskConstants.TASK_BACKGROUND_ALPHA;
                if (item.DueDate < DateTime.Today)
                {
                    taskDueDate.SetTextColor(new Color(ContextCompat.GetColor(Context, Resource.Color.light_red)));
                }
                else
                {
                    taskDueDate.SetTextColor(new Color(ContextCompat.GetColor(Context, Resource.Color.black)));
                }
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

            taskDueDate.Text = DateTimeConverter.DateToString(item.DueDate);


            if (item.ProjectID != 0)
            {
                taskProject.Text = _projects.First((x)=> x.ID==item.ProjectID).Title;
            }
            else
            {
                taskProject.Text = Context.GetString(Resource.String.project_inbox);
            }
                        
            return view;
        }

        public Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }
    }
}