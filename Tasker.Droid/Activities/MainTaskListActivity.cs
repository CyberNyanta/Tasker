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

using Firebase.Auth;
using Firebase.Database;

namespace Tasker.Droid.Activities
{
    [Activity(Label = "Tasker", MainLauncher = true, Theme = "@style/Tasker")]
    public class MainTaskListActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        DrawerLayout _drawer;
        ActionBarDrawerToggle _toggle;
        private ISharedPreferences _sharedPreferences;
        private FirebaseAuth mFirebaseAuth;
        private FirebaseUser mFirebaseUser;
        private string mUsername;
        private string mPhotoUrl;
        private DatabaseReference mFirebaseDatabaseReference;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            mFirebaseAuth = FirebaseAuth.Instance;
            mFirebaseUser = mFirebaseAuth.CurrentUser;
            if (mFirebaseUser == null)
            {
                // Not signed in, launch the Sign In activity
                StartActivity(new Intent(this, typeof(SignInActivity)));
                Finish();
                return;
            }
            else
            {
                mUsername = mFirebaseUser.DisplayName;
                if (mFirebaseUser.PhotoUrl != null)
                {
                    mPhotoUrl = mFirebaseUser.PhotoUrl.ToString();
                }
            }

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            int menuIndex = 0;
            SupportActionBar.Title = GetString(Resource.String.navigation_all);
            if (bundle == null)
            {
                _sharedPreferences = GetSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private);
                menuIndex = _sharedPreferences.GetInt(GetString(Resource.String.settings_start_page), 0);
                StartFragment((StartScreens)menuIndex);
            }

            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _toggle = new ActionBarDrawerToggle(this, _drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            _drawer.AddDrawerListener(_toggle);
            _toggle.SyncState();
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.Menu.GetItem(menuIndex).SetChecked(true);
        }

        public void StartFragment(StartScreens type)
        {

            switch (type)
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

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_all:
                    StartFragment(StartScreens.AllTask);
                    break;
                case Resource.Id.navigation_inbox:
                    StartFragment(StartScreens.Inbox);
                    break;
                case Resource.Id.navigation_today:
                    StartFragment(StartScreens.Today);
                    break;
                case Resource.Id.navigation_tomorrow:
                    StartFragment(StartScreens.Tomorrow);
                    break;
                case Resource.Id.navigation_nextWeek:
                    StartFragment(StartScreens.NextWeek);
                    break;
                case Resource.Id.navigation_projects:
                    SupportActionBar.Title = GetString(Resource.String.navigation_projects);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new ProjectListFragment()).Commit();
                    break;
                case Resource.Id.navigation_settings:
                    SupportActionBar.Title = GetString(Resource.String.settings);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new SettingsFragment()).Commit();
                    break;
                case Resource.Id.navigation_statistics:
                    SupportActionBar.Title = GetString(Resource.String.statistics);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new StatisticsFragment()).Commit();
                    break;
            }
            _drawer.CloseDrawers();
            return true;
        }
    }
}


