using Android.App;
using Android.Content;

namespace Tasker.Droid.AL.Utils
{
    [BroadcastReceiver(Enabled = true)]
    public class RemindAlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent paramIntent)
        {
            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            Notification notification = (Notification)paramIntent.GetParcelableExtra(IntentExtraConstants.REMINDER_NOTIFICATION_EXTRA);
            if (notification != null)
                notificationManager.Notify(0, notification);
        }
    }
}