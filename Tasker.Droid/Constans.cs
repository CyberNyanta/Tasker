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

namespace Tasker.Droid
{
    static class Constans
    {
        public const string DATABASE_NAME = "TaskDB.db3";
    }

    static class IntentExtraConstants
    {
        public const string PROJECT_ID_EXTRA = "PROJECT_ID_EXTRA";
        public const string TASK_ID_EXTRA = "TASK_ID_EXTRA";    
        public const string DUE_DATE_TYPE_EXTRA = "DUE_DATE_TYPE_EXTRA";    
        public const string TASK_LIST_TYPE_EXTRA = "TASK_LIST_TYPE_EXTRA";
        public const string IS_SEARCH_IN_PROJECT_EXTRA = "IS_SEARCH_IN_PROJECT_EXTRA";
        public const string REMINDER_NOTIFICATION_EXTRA = "REMINDER_NOTIFICATION_EXTRA";
        public const string TASK_COLOR_EXTRA = "TASK_COLOR_EXTRA";
    }
}