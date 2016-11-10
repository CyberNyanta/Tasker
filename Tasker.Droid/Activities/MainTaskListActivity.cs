using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

using Tasker.Droid.Fragments;
using Tasker.Core;
using TinyIoC;
using Tasker.Core.AL.ViewModels.Contracts;

namespace Tasker.Droid.Activities
{
    [Activity (Label = "Tasker", MainLauncher = true, Theme = "@style/Tasker")]
    public class MainTaskListActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        DrawerLayout _drawer;
        ActionBarDrawerToggle _toggle;
        private ISharedPreferences _sharedPreferences;

        protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = GetString(Resource.String.navigation_all);
            if (bundle == null)
            {
                _sharedPreferences = GetSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private);
                var startscreen = (StartScreens)_sharedPreferences.GetInt(GetString(Resource.String.settings_start_page), 0);
                switch (startscreen)
                {
                    case StartScreens.AllTask:
                        Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                        Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.AllOpen);
                        SupportActionBar.Title = GetString(Resource.String.navigation_all);
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                        break;
                    case StartScreens.Inbox:
                        Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                        Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.ProjectOpen);
                        SupportActionBar.Title = GetString(Resource.String.navigation_inbox);
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                        break;
                    case StartScreens.Today:
                        Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                        Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.Today);
                        SupportActionBar.Title = GetString(Resource.String.navigation_today);
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                        break;
                    case StartScreens.Tomorrow:
                        Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                        Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.Tomorrow);
                        SupportActionBar.Title = GetString(Resource.String.navigation_tomorrow);
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                        break;
                    case StartScreens.NextWeek:
                        Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                        Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.NextWeek);
                        SupportActionBar.Title = GetString(Resource.String.navigation_nextWeek);
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                        break;
                    case StartScreens.SelectedProject:
                        var viewModel = TinyIoCContainer.Current.Resolve<IProjectDetailsViewModel>();
                        viewModel.Id = _sharedPreferences.GetInt(GetString(Resource.String.project), 0);
                        Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, viewModel.Id);
                        Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.ProjectOpen);      
                        var project = viewModel.GetItem();
                        SupportActionBar.Title = project.Title;
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                        break;
                }               
            }

            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _toggle = new ActionBarDrawerToggle( this, _drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            _drawer.AddDrawerListener(_toggle);
            _toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.Menu.GetItem(0).SetChecked(true);    
       
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_all:
                    Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                    Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.AllOpen);
                    SupportActionBar.Title = GetString(Resource.String.navigation_all);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_inbox:
                    Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                    Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.ProjectOpen);
                    SupportActionBar.Title = GetString(Resource.String.navigation_inbox);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_today:
                    Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                    Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.Today);
                    SupportActionBar.Title = GetString(Resource.String.navigation_today);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_tomorrow:
                    Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                    Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.Tomorrow);
                    SupportActionBar.Title = GetString(Resource.String.navigation_tomorrow);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_nextWeek:
                    Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                    Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.NextWeek);
                    SupportActionBar.Title = GetString(Resource.String.navigation_nextWeek);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_projects:
                    SupportActionBar.Title = GetString(Resource.String.navigation_projects);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new ProjectListFragment()).Commit();
                    break;
                case Resource.Id.navigation_settings:
                    SupportActionBar.Title = GetString(Resource.String.settings);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new SettingsFragment()).Commit();
                    break;
            }            
            _drawer.CloseDrawers();
            return true;
        }
    }
}


