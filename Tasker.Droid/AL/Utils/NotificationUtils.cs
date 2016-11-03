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

using Tasker.Droid.Activities;
using Tasker.Core.DAL.Entities;

namespace Tasker.Droid.AL.Utils
{
    class NotificationUtils
    {
        private readonly Intent _remindReceiverIntent;
        private readonly Intent _taskEditCreateIntent;

        public NotificationUtils()
        {
            _remindReceiverIntent = new Intent(Application.Context, typeof(RemindAlarmReceiver));
            _taskEditCreateIntent = new Intent(Application.Context, typeof(TaskEditCreateActivity));
        }
        public void SetTaskReminder(Task task)
        {
            AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            int id = task.ID;           
            PendingIntent pendingIntent2 = PendingIntent.GetActivity(Application.Context, id, _taskEditCreateIntent, PendingIntentFlags.UpdateCurrent);
            Notification.Builder builder = new Notification.Builder(Application.Context)
               .SetContentIntent(pendingIntent2)
               .SetContentTitle(task.Title)
               .SetContentText(task.DueDate != DateTime.MaxValue ? task.DueDate.ToString(Application.Context.GetString(Resource.String.datetime_regex)) : "")
               .SetSmallIcon(Resource.Drawable.ic_delete_light);
            var notification = builder.Build();

            _remindReceiverIntent.PutExtra(IntentExtraConstants.REMINDER_NOTIFICATION_EXTRA, notification);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, id, _remindReceiverIntent, PendingIntentFlags.UpdateCurrent);
            if (task.RemindDate != DateTime.MaxValue)
            {
                long trigerMilliseconds = SystemClock.ElapsedRealtime() + (long)(task.RemindDate - DateTime.Now).TotalMilliseconds;
                alarmManager.SetExact(AlarmType.ElapsedRealtimeWakeup, trigerMilliseconds, pendingIntent);
            }
            else
            {
                alarmManager.Cancel(pendingIntent);
            }
        }
    }
}