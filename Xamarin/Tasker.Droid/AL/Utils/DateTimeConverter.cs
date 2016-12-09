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
        public static string DateToString(DateTime dueDate)
        {

            var context = Application.Context;
            var is24hoursTimeFormate = context.GetSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private)
                .GetBoolean(context.GetString(Resource.String.settings_24hours_format), false);
            if (dueDate != DateTime.MaxValue)
            {
                if (dueDate == DateTime.Today)
                {
                    return context.GetString(Resource.String.due_dates_today);
                }
                else if(dueDate.Date == DateTime.Today)
                {
                    return context.GetString(Resource.String.due_dates_today_at, GetLocaleTime(dueDate, is24hoursTimeFormate));
                }
                else if (dueDate == DateTime.Today.AddDays(1))
                {
                    return context.GetString(Resource.String.due_dates_tomorrow);
                }
                else if (dueDate.Date == DateTime.Today.AddDays(1))
                {
                    return context.GetString(Resource.String.due_dates_tomorrow_at, GetLocaleTime(dueDate, is24hoursTimeFormate));
                }
                else if (dueDate > DateTime.Today && dueDate < DateTime.Today.AddDays(8))
                {
                    return $"{dueDate.ToString(context.GetString(Resource.String.datetime_format_date))}, {GetLocaleTime(dueDate, is24hoursTimeFormate)}";
                }
                else
                {
                    return dueDate.ToString(dueDate.Year == DateTime.Today.Year ? context.GetString(Resource.String.datetime_format_date)
                        : context.GetString(Resource.String.datetime_format_date_year));
                }
            }
            else
            {
                return context.GetString(Resource.String.datetime_none);
            }
        }

        public static string GetLocaleTime(DateTime dueDate, bool is24hoursTimeFormate)
        {
            return is24hoursTimeFormate ? dueDate.ToString(Application.Context.GetString(Resource.String.time_format_24)) 
                : dueDate.ToString(Application.Context.GetString(Resource.String.time_format_12));
        }
    }
}