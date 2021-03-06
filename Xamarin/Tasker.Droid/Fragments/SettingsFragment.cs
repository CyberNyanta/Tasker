
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Droid.Adapters;


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
        private IProjectListViewModel _viewModel;
        private StartScreens _startScreen;
        private string _startScreenName;
        private int _projectId =0;

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
            _viewModel = TinyIoCContainer.Current.Resolve<IProjectListViewModel>();
            _pushNotificatin = view.FindViewById<Switch>(Resource.Id.setting_notification);
            _24hoursFormat = view.FindViewById<Switch>(Resource.Id.setting_24time);
            _startPage = view.FindViewById<View>(Resource.Id.setting_start_page);
            _startPageTitle = view.FindViewById<TextView>(Resource.Id.setting_start_page_title);
            _startPageCurrent = view.FindViewById<TextView>(Resource.Id.setting_start_page_current);

            _pushNotificatin.Checked = _sharedPreferences.GetBoolean(GetString(Resource.String.settings_push_notifications), _pushNotificatin.Checked);
            _24hoursFormat.Checked = _sharedPreferences.GetBoolean(GetString(Resource.String.settings_push_notifications), _24hoursFormat.Checked);
            _startScreenName = _sharedPreferences.GetString(GetString(Resource.String.settings_start_page_name), GetString(Resource.String.navigation_all));
            _startPageCurrent.Text = _startScreenName;
            _startPage.Click += (o, args)=>{ SetStartPage(); };
        }

        public override void OnStop()
        {
            _sharedPreferences.Edit()
                .PutBoolean(GetString(Resource.String.settings_push_notifications), _pushNotificatin.Checked)
                .PutBoolean(GetString(Resource.String.settings_24hours_format), _24hoursFormat.Checked)
                .PutInt(GetString(Resource.String.settings_start_page), (int)_startScreen)
                .PutString(GetString(Resource.String.settings_start_page_name), _startScreenName)
                .PutInt(GetString(Resource.String.project), _projectId)
                .Commit();
            base.OnDestroy();
        }

        private void SetStartPage()
        {
            Dialog dialog = null;
            var list = (ExpandableListView) Activity.LayoutInflater.Inflate(Resource.Layout.expandable_list, null);
            list.SetAdapter(new StartPageDialogAdapter(Activity, _viewModel.GetAll()));
            list.ChildClick += (sender, args) =>
            {
                dialog.Cancel();
                switch (args.GroupPosition)
                {
                    case 0:
                        _startScreen = (StartScreens)args.ChildPosition;
                        _startScreenName = _startScreen.ToLocalString();
                        _startPageCurrent.Text = _startScreenName;
                        break;
                    case 1:
                        _projectId = (int)args.Id;
                        _startScreen = StartScreens.SelectedProject;
                        _startScreenName = _viewModel.GetItem(_projectId).Title;
                        _startPageCurrent.Text = _startScreenName;                     
                        break;
                }
            };
            
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            dialog = alert.SetView(list)
                          .SetCancelable(true)
                          .Show();
        }    
    }
}