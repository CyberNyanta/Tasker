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

using Tasker.Core.AL.ViewModels.Contracts;
using Android.Views.Animations;
using Tasker.Core.DAL.Entities;
using Tasker.Droid.Activities;

namespace Tasker.Droid.Fragments
{
    public class BaseListFragment : Fragment
    {
        private FAB _fab;
        private int previousVisibleItem;
        protected ListView _listView = null;


        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _fab = view.FindViewById<FAB>(Resource.Id.fab);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _fab.Show(false);
            _fab.PostDelayed(ShowFab, 300);
            _fab.Click += FabClick;      
        }

        public void HideFAB()
        {
            _fab.Visibility = ViewStates.Gone;
        }


        protected virtual void FabClick(object sender, EventArgs e)
        {
        }


        public override void OnPause()
        {
            base.OnPause();
            _listView.Scroll -= ListView_Scroll;
        }

        public override void OnResume()
        {
            base.OnResume();
            _listView.Scroll += ListView_Scroll;
        }


        private void ShowFab()
        {
            _fab.Show(true);
            _fab.SetShowAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.show_from_bottom));
            _fab.SetHideAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.hide_to_bottom));
        }

        private void ListView_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {

            if (e.FirstVisibleItem > previousVisibleItem)
            {
                _fab.Hide(true);
            }
            else if (e.FirstVisibleItem < previousVisibleItem)
            {
                _fab.Show(true);
            }
            previousVisibleItem = e.FirstVisibleItem;
        }
    }
}