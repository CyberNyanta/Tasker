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
using Tasker.Droid.AL.Utils;
using System.Globalization;

namespace Tasker.Droid.Adapters
{
    public class TaskListFor7DaysAdapter : TaskListAdapter
    {

        public TaskListFor7DaysAdapter(Activity context, List<Task> tasks, List<Project> projects) : base(context, tasks, projects)
        {
            TaskList.Insert(0, new Task {Title = context.GetString(Resource.String.today), DueDate = DateTime.MinValue });
            TaskList.Insert(1, new Task { Title = context.GetString(Resource.String.tomorrow), DueDate = DateTime.Today.AddDays(2).AddSeconds(-1) });
            int dayOfWeek = (int)DateTime.Today.DayOfWeek;
            for (int i = 0; i < 5; i++)
            {
                var sdfgi = (i + dayOfWeek) % 7;
                TaskList.Insert(2, new Task { Title = DateTimeFormatInfo.CurrentInfo.DayNames[(i+ dayOfWeek)% 7], DueDate = DateTime.Today.AddDays(3+i).AddSeconds(-1) });
            }
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
            return TaskList[position] != null ? 0 : 1;
        }
        public  override View GetView(int position, View convertView, ViewGroup parent)
        {
            return base.GetView(position, convertView, parent);
            if (GetItemViewType(position) == 0)
            {
                return base.GetView(position, convertView, parent);
            }else
            {
                throw new NotImplementedException();
 
            }
        }
    }
}