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
using Tasker.Core.DAL.Entities;

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
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}