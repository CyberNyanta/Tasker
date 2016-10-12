using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.Support.V7.App.AlertDialog;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.AL.Utils;
using Tasker.Core.DAL.Entities;
using Tasker.Core;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using Java.Util;
using Android.Graphics;
using FAB = Clans.Fab.FloatingActionButton;

namespace Tasker.Droid.Activities
{
    [Activity]
    public class TaskDetailsActivity : AppCompatActivity
    {
        private ITaskDetailsViewModel _viewModel;
        private TextView _taskTitle;
        private TextView _taskDescription;
        private TextView _taskDueDate;
        private TextView _taskRemindDate;
        private View _taskColor;
        private FAB _fab;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.task_details);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            

            _viewModel = TinyIoCContainer.Current.Resolve<ITaskDetailsViewModel>();

            _taskTitle = FindViewById<TextView>(Resource.Id.task_title);
            _taskDescription = FindViewById<TextView>(Resource.Id.task_description);
            _taskDueDate = FindViewById<TextView>(Resource.Id.task_dueDate);
            _taskRemindDate = FindViewById<TextView>(Resource.Id.task_remindDate);
            _taskColor = FindViewById<View>(Resource.Id.task_color_border);
            _fab = FindViewById<FAB>(Resource.Id.fab);

            _viewModel.Id = Intent.GetIntExtra("TaskId", 0);
            _fab.Click += delegate
            {
                OnSolveClick();
            };

            Initialization();
        }

        private void Initialization()
        {
            var task = _viewModel.GetItem();
            if (task != null)
            {
                _taskTitle.Text = task.Title;
                _taskDescription.Text = task.Description;
                _taskDueDate.Text = task.DueDate.ToString();
                _taskRemindDate.Text = task.RemindDate.ToString();
                _fab.SetImageResource(task.IsSolved ? Resource.Drawable.fab_unsolved : Resource.Drawable.fab_solved);
                if (task.Color != TaskColors.None)
                {
                    _taskColor.SetBackgroundColor(Color.ParseColor(TaskConstants.Colors[(int)task.Color]));
                }
                else
                {
                    _taskColor.Background = _taskDescription.Background;
                }

                _taskDescription.Visibility = string.IsNullOrWhiteSpace(task.Description) ? ViewStates.Gone : ViewStates.Visible;
                _taskDueDate.Visibility = task.DueDate==DateTime.MinValue? ViewStates.Gone : ViewStates.Visible;
                _taskRemindDate.Visibility = task.RemindDate == DateTime.MinValue ? ViewStates.Gone : ViewStates.Visible;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            Initialization();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.task_details_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                case Resource.Id.menu_edit:
                    OnEditClick();
                    break;
                case Resource.Id.menu_delete:
                    OnDeleteClick();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        private void OnEditClick()
        {
            Intent intent = new Intent(this, typeof(TaskEditCreateActivity));
            intent.PutExtra("TaskId", _viewModel.Id);
            StartActivity(intent);            
        }

        private void OnSolveClick()
        {
            var task = _viewModel.GetItem();
            if (task.IsSolved)
            {
                _viewModel.ChangeStatus(task);
                _fab.SetImageResource(Resource.Drawable.fab_unsolved);          
                SetResult(Result.Ok);
            }
            else
            {
                _viewModel.ChangeStatus(task);
                _fab.SetImageResource(Resource.Drawable.fab_solved);
                SetResult(Result.Ok);
            }
        }
        private void OnDeleteClick()
        {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(GetString(Resource.String.confirm_delete_task));
            alert.SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) =>
            {
                _viewModel.DeleteItem();
                SetResult(Result.Ok);
                Finish();
            });
            alert.SetCancelable(true);
            alert.SetNegativeButton(GetString(Resource.String.dialog_cancel), (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

   
    }
}