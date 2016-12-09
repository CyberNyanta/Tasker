using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using Tasker.Core.DAL.Entities;

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
            if (tasks != null)
            {
                var inboxProjectTasks = tasks.FindAll(task => task.ProjectID == 0);
                _projects.Insert(0, new Project
                {
                    Title = context.GetString(Resource.String.project_inbox),
                    CountOfCompletedTasks = inboxProjectTasks.FindAll(task => task.IsCompleted).Count,
                    CountOfOpenTasks = inboxProjectTasks.FindAll(task => !task.IsCompleted).Count
                });
            }
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

        public void Remove(int position)
        {
            _projects.RemoveAt(position);
            NotifyDataSetChanged();
        }

        public void Add(Project project)
        {
            _projects.Add(project);
            NotifyDataSetChanged();
        }

        public void Save(Project project, int position)
        {
            _projects.RemoveAt(position);
            _projects.Add(project);
            NotifyDataSetChanged();
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
                view = _context.LayoutInflater.Inflate(Resource.Layout.project_list_item, null);
            }
            else
            {
                view = convertView;
            }

            var projectTitle = view.FindViewById<TextView>(Resource.Id.project_title);
            var projectTaskCount = view.FindViewById<TextView>(Resource.Id.task_count);
            
            projectTaskCount.Text = _context.GetString(Resource.String.task_count, item.CountOfCompletedTasks, item.CountOfOpenTasks + item.CountOfCompletedTasks);
            projectTitle.Text = item.Title;

            //Finally return the view
            return view;
        }

    }
}