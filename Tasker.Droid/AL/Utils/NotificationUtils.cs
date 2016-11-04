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
using Tasker.Core.AL.Utils.Contracts;

namespace Tasker.Droid.AL.Utils
{
    public class NotificationUtils: INotificationUtils
    {
        private readonly Intent _remindReceiverIntent;
        private readonly Intent _taskEditCreateIntent;
        private readonly AlarmManager _alarmManager;

        public NotificationUtils()
        {
            _remindReceiverIntent = new Intent(Application.Context, typeof(RemindAlarmReceiver));
            _taskEditCreateIntent = new Intent(Application.Context, typeof(TaskEditCreateActivity));
            _alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
        }
        public void SetTaskReminder(Task task)
        {            
            _taskEditCreateIntent.PutExtra(IntentExtraConstants.TASK_ID_EXTRA, task.ID);
            PendingIntent pendingIntent2 = PendingIntent.GetActivity(Application.Context, task.ID, _taskEditCreateIntent, PendingIntentFlags.UpdateCurrent);
            var notification = new Notification.Builder(Application.Context)
               .SetContentIntent(pendingIntent2)
               .SetContentTitle(task.Title)
               .SetContentText(task.DueDate != DateTime.MaxValue ? task.DueDate.ToString(Application.Context.GetString(Resource.String.datetime_regex)) : "")
               .SetSmallIcon(Resource.Drawable.ic_remind_light)
               .Build();

            notification.Flags = NotificationFlags.AutoCancel | NotificationFlags.ShowLights;

            _remindReceiverIntent.PutExtra(IntentExtraConstants.REMINDER_NOTIFICATION_EXTRA, notification);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, task.ID, _remindReceiverIntent, PendingIntentFlags.UpdateCurrent);
            var r = DateTime.Now;
            if (task.RemindDate != DateTime.MaxValue && task.RemindDate >= DateTime.Now)
            {
                long trigerMilliseconds = SystemClock.ElapsedRealtime() + (long)(task.RemindDate - DateTime.Now).TotalMilliseconds;
                _alarmManager.SetExact(AlarmType.ElapsedRealtimeWakeup, trigerMilliseconds, pendingIntent);
            }
            else
            {
                _alarmManager.Cancel(pendingIntent);
            }
        }

        public void RemoveTaskReminder(Task task)
        {
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, task.ID, _remindReceiverIntent, PendingIntentFlags.UpdateCurrent);
            _alarmManager.Cancel(pendingIntent);
        }

    }
}