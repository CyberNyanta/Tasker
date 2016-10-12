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
    public class ProjectListAdapter : BaseAdapter<Project>
    {
        private Activity _context;
        private List<Project> _projects;

        public ProjectListAdapter(Activity context, List<Task> tasks, List<Project> projects) : base()
        {
            _context = context;
            _projects = projects;
            var inboxProjectTasks = tasks.FindAll(task => task.ProjectID == 0);
            _projects.Insert(0, new Project
            {
                Title = context.GetString(Resource.String.project_inbox),
                CountOfOpenTasks = inboxProjectTasks.FindAll(task => task.IsSolved).Count,
                CountOfSolveTasks = inboxProjectTasks.FindAll(task => !task.IsSolved).Count
            });
        }

        public override Project this[int position]
        {
            get { return _projects[position]; }
        }

        public override int Count
        {
            get { return _projects.Count; }
        }

        public override long GetItemId(int position)
        {
            return _projects[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            var item = _projects[position];
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

            var projectTitle = view.FindViewById<TextView>(Resource.Id.project_title);
            var projectTaskCount = view.FindViewById<TextView>(Resource.Id.task_count);
            var editButton = view.FindViewById<ImageButton>(Resource.Id.edit);
            var deleteButton = view.FindViewById<ImageButton>(Resource.Id.delete);

            projectTaskCount.Text = $"{item.CountOfOpenTasks}/{item.CountOfOpenTasks + item.CountOfSolveTasks}";
            projectTitle.Text = item.Title;
            if (item.ID != 0)
            {
                editButton.Visibility = ViewStates.Visible;
                deleteButton.Visibility = ViewStates.Visible;
                editButton.Click -= EditButtonClick;
                deleteButton.Click -= DeleteButtonClick;
                editButton.Click += EditButtonClick;
                deleteButton.Click += DeleteButtonClick;
                editButton.Tag = item.ID;
                deleteButton.Tag = item.ID;
            }
            else
            {
                editButton.Visibility = ViewStates.Gone;
                deleteButton.Visibility = ViewStates.Gone;
            }

            //Finally return the view
            return view;
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            int id = (int)button.Tag;
            
        }

        private void EditButtonClick(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            int id = (int)button.Tag;
        }
    }
}