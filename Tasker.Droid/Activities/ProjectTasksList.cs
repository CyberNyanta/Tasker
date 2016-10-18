using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views.Animations;

using Java.Util;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using FAB = Clans.Fab.FloatingActionButton;

using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Entities;
using Tasker.Core.AL.Utils;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Droid.Fragments;
using Android.Graphics;

namespace Tasker.Droid
{
    [Activity (Label = "Tasker",  Theme = "@style/Tasker")]
    public class ProjectTasksList : AppCompatActivity
    {
        protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);
			SetContentView (Resource.Layout.main);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            if (bundle == null)
            {
                SupportFragmentManager.BeginTransaction().Add(Resource.Id.fragment, new TaskListFragment()).Commit();
            }            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}


