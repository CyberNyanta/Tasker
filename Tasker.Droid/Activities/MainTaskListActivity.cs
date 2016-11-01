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

using System;

namespace Tasker.Droid.Activities
{
    [Activity (Label = "Tasker", MainLauncher = true, Theme = "@style/Tasker")]
    public class MainTaskListActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        DrawerLayout _drawer;
        ActionBarDrawerToggle _toggle;
        protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = GetString(Resource.String.app_name);

            if (bundle == null)
            {
                SupportFragmentManager.BeginTransaction().Add(Resource.Id.fragment, new TaskListFragment()).Commit();
            }

            _drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _toggle = new ActionBarDrawerToggle( this, _drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            _drawer.AddDrawerListener(_toggle);
            _toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_activity_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_projects:
                    var intent = new Intent(this, typeof(ProjectListActivity));
                    StartActivity(intent);
                    break;             
            }
            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_all:
                    Intent.PutExtra("ProjectId", 0);
                    Intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.AllOpen);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_inbox:
                    Intent.PutExtra("ProjectId", 0);
                    Intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.ProjectOpen);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_today:
                    Intent.PutExtra("ProjectId", 0);
                    Intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.Today);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_tomorrow:
                    Intent.PutExtra("ProjectId", 0);
                    Intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.Tomorrow);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
                case Resource.Id.navigation_nextWeek:
                    Intent.PutExtra("ProjectId", 0);
                    Intent.PutExtra("TaskListType", (int)TaskListFragment.TaskListType.NextWeek);
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();
                    break;
            }
            _drawer.CloseDrawers();
            return true;
        }
    }
}


