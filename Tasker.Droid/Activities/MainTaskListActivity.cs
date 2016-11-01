using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

using Tasker.Droid.Fragments;

namespace Tasker.Droid.Activities
{
    [Activity (Label = "Tasker", MainLauncher = true, Theme = "@style/Tasker")]
    public class MainTaskListActivity : AppCompatActivity
    {
        private DrawerLayout mDrawerLayout;
        private ListView mDrawerList;

        protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);
			SetContentView (Resource.Layout.main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = GetString(Resource.String.app_name);

            if (bundle == null)
            {
                SupportFragmentManager.BeginTransaction().Add(Resource.Id.fragment, new TaskListFragment()).Commit();
            }            
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

    }
}


