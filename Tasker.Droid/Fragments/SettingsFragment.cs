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

using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;
using FAB = Clans.Fab.FloatingActionButton;
using Com.Wdullaer.Swipeactionadapter;

using Tasker.Core.AL.ViewModels.Contracts;
using Android.Views.Animations;
using Tasker.Core.DAL.Entities;
using Tasker.Core.AL.Utils;
using Tasker.Core;
using Tasker.Droid.Activities;

namespace Tasker.Droid.Fragments
{
    public class SettingsFragment : Fragment
    {
        private Switch _pushNotificatin;
        private Switch _24hoursFormat;
        private View _startPage;
        private TextView _startPageTitle;
        private TextView _startPageCurrent;
        private ISharedPreferences _sharedPreferences;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            return inflater.Inflate(Resource.Layout.settings, container, false);  
        }

        public override void OnPrepareOptionsMenu(IMenu menu)
        {
            menu.Clear();
            base.OnPrepareOptionsMenu(menu);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _sharedPreferences = Activity.GetSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private);
                       
            _pushNotificatin = view.FindViewById<Switch>(Resource.Id.setting_notification);
            _24hoursFormat = view.FindViewById<Switch>(Resource.Id.setting_24time);
            _startPage = view.FindViewById<View>(Resource.Id.setting_start_page);
            _startPageTitle = view.FindViewById<TextView>(Resource.Id.setting_start_page_title);
            _startPageCurrent = view.FindViewById<TextView>(Resource.Id.setting_start_page_current);

            _pushNotificatin.Checked = _sharedPreferences.GetBoolean(GetString(Resource.String.settings_push_notifications), _pushNotificatin.Checked);
            _24hoursFormat.Checked = _sharedPreferences.GetBoolean(GetString(Resource.String.settings_push_notifications), _24hoursFormat.Checked);
        }                     

        public override void OnDestroy()
        {
            _sharedPreferences.Edit()
                .PutBoolean(GetString(Resource.String.settings_push_notifications), _pushNotificatin.Checked)
                .PutBoolean(GetString(Resource.String.settings_24hours_format), _24hoursFormat.Checked)
                .PutInt(GetString(Resource.String.settings_start_page), 2)
                .Commit();
            base.OnDestroy();
        }

        private void DeleteProject(Action callback)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            alert.SetTitle(GetString(Resource.String.confirm_delete_project))
                 .SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) => { callback?.Invoke(); })
                 .SetCancelable(true)
                 .SetNegativeButton(GetString(Resource.String.dialog_cancel), (senderAlert, args) => { })
                 .Show();
        }    
    }
}