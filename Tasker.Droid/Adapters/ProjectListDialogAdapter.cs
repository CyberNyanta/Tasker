using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using Tasker.Core.DAL.Entities;

namespace Tasker.Droid.Adapters
{
    public class ProjectListDialogAdapter : BaseAdapter<Project>
    {
        private Activity _context;
        private List<Project> _projects;
        private int _current;
        private event Action OnClick;

        public ProjectListDialogAdapter(Activity context, List<Project> projects, int current, Action callback) : base()
        {
            _context = context;
            _current = current;
            _projects = projects;
            OnClick += callback;
        }

        public override int Count
        {
            get { return _projects.Count; }
        }

        public override Project this[int position]
        {
            get { return _projects[position]; }
        }

        public override long GetItemId(int position)
        {
            return (long)_projects[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var item = _projects[position];
            View view;
            view = _context.LayoutInflater.Inflate(Resource.Layout.project_list_item_dialog, null);
            if (Enum.Equals(item.ID,_current))
            {
                view.SetBackgroundResource(Resource.Color.item_selected);
            }
            var textView = view.FindViewById<TextView>(Resource.Id.project_title);
            textView.Text = item.Title;
            textView.Click += View_Click;
            view.Click += View_Click;
            view.Tag = item.ID;
            return view;
        }

        private void View_Click(object sender, EventArgs e)
        {
            _context.Intent.PutExtra("ProjectId", (int)((View)sender).Tag);      

            _context.RunOnUiThread(() =>
            {
                OnClick?.Invoke();
            });
            
        }
    }
}