﻿using System;
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
    [Activity (Label = "Tasker", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Tasker")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);
			SetContentView (Resource.Layout.main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
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
    }
}


