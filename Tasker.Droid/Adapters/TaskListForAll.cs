using System;
using System.Collections.Generic;
using System.Globalization;

using Android.App;
using Android.Views;
using Android.Widget;

using Tasker.Core.DAL.Entities;


namespace Tasker.Droid.Adapters
{
    public class TaskListAll : TaskListAdapter
    {

        public TaskListAll(Activity context, List<Task> tasks, List<Project> projects) : base(context, tasks, projects)
        {
            TaskList.Insert(0, new Task
            {
                Title = context.GetString(Resource.String.today),
                DueDate = DateTime.MinValue,
                Description = DateTime.Today.ToString("ddd d MMM")
            });  //Today
            TaskList.Insert(1, new Task
            {
                Title = context.GetString(Resource.String.tomorrow),
                DueDate = DateTime.Today.AddDays(1).AddSeconds(-1),
                Description = DateTime.Today.AddDays(1).ToString("ddd d MMM")
            }); // Tomorrow

            TaskList.Insert(2, new Task
            {
                Title = context.GetString(Resource.String.next7days),
                DueDate = DateTime.Today.AddDays(2).AddSeconds(-1),
                Description = $"{ DateTime.Today.AddDays(2).ToString("d MMM")} - { DateTime.Today.AddDays(8).ToString("d MMM")}"
            }); // Next 7 days

            TaskList.Insert(2, new Task
            {
                Title = context.GetString(Resource.String.rest),
                DueDate = DateTime.Today.AddDays(9).AddSeconds(-1),
                Description = ""
            }); // rest


            TaskList.Sort((t1, t2) => DateTime.Compare(t1.DueDate, t2.DueDate));
        }

        public override int ViewTypeCount
        {
            get
            {
                return 2;
            }
        }
        
        public override int GetItemViewType(int position)
        {
            return TaskList[position].ID != 0 ? 0 : 1;
        }
        public  override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            if (GetItemViewType(position) == 0)
            {
                return base.GetView(position, convertView, parent);
            }else
            {
                View view;
                if (convertView == null)
                {
                    view = Context.LayoutInflater.Inflate(Resource.Layout.task_list_header, null);
                }
                else
                {
                    view = convertView;
                }
                var right = parent.FindViewById(Resource.Id.task_background_right);
                var left = parent.FindViewById(Resource.Id.task_background_left);
                if (position + 1 < TaskList.Count && TaskList[position + 1].ID != 0)
                {
                    var header = view.FindViewById<TextView>(Resource.Id.header);
                    var headerDate = view.FindViewById<TextView>(Resource.Id.header_date);
                    header.Text = TaskList[position].Title;
                    headerDate.Text = TaskList[position].Description;
                    view.Visibility = ViewStates.Visible;
                    right.Visibility = ViewStates.Visible;
                    left.Visibility = ViewStates.Visible;
                }
                else
                {
                    view.Visibility = ViewStates.Gone;
                    right.Visibility = ViewStates.Gone;
                    left.Visibility = ViewStates.Gone;
                }
                return view;
            }
        }
    }
}