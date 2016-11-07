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

namespace Tasker.Droid.AL.Utils
{
    public static class DateTimeConverter
    {
        public static string DueDateToString(DateTime dueDate)
        {
            if (dueDate != DateTime.MaxValue)
            {
                if (dueDate == DateTime.Today)
                {
                    return Application.Context.GetString(Resource.String.due_dates_today);
                }
                if (dueDate.Date == DateTime.Today)
                {
                    return Application.Context.GetString(Resource.String.due_dates_today_at, dueDate.ToString(Application.Context.GetString(Resource.String.time_regex)));
                }
                else if (dueDate == DateTime.Today.AddDays(1))
                {
                    return Application.Context.GetString(Resource.String.due_dates_tomorrow);
                }
                else if (dueDate.Date == DateTime.Today.AddDays(1))
                {
                    return Application.Context.GetString(Resource.String.due_dates_tomorrow_at, dueDate.ToString(Application.Context.GetString(Resource.String.time_regex)));
                }
                else
                {
                    return dueDate.ToString(Application.Context.GetString(Resource.String.datetime_regex));
                }
            }
            else
            {
                return "";
            }
        }
    }
}