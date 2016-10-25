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
        private LinearLayout _taskDueDateConteiner;
        private LinearLayout _taskRemindDateConteiner;
        private LinearLayout _taskDescriptionConteiner;
        private View _taskColor;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.task_details);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.Title = GetString(Resource.String.task_details_title);

            _viewModel = TinyIoCContainer.Current.Resolve<ITaskDetailsViewModel>();

            _taskTitle = FindViewById<TextView>(Resource.Id.task_title);
            _taskDescription = FindViewById<TextView>(Resource.Id.task_description);
            _taskDueDate = FindViewById<TextView>(Resource.Id.task_dueDate);
            _taskRemindDate = FindViewById<TextView>(Resource.Id.task_remindDate);
            _taskColor = FindViewById<View>(Resource.Id.task_color_border);
            _taskDueDateConteiner = FindViewById<LinearLayout>(Resource.Id.box_task_due_date);
            _taskRemindDateConteiner = FindViewById<LinearLayout>(Resource.Id.box_task_remind_date);
            _taskDescriptionConteiner = FindViewById<LinearLayout>(Resource.Id.box_task_description);

            _viewModel.Id = Intent.GetIntExtra("TaskId", 0);

            Initialization();
        }

        private void Initialization()
        {
            var task = _viewModel.GetItem(_viewModel.Id);
            if (task != null)
            {
                _taskTitle.Text = task.Title;
                _taskDescription.Text = task.Description;
                _taskDueDate.Text = task.DueDate.ToString();
                _taskRemindDate.Text = task.RemindDate.ToString();

                if (task.Color != TaskColors.None)
                {
                    _taskColor.SetBackgroundColor(Color.ParseColor(TaskConstants.Colors[task.Color]));
                }
                else
                {
                    _taskColor.Background = _taskDescription.Background;
                }

                _taskDescriptionConteiner.Visibility = string.IsNullOrWhiteSpace(task.Description) ? ViewStates.Gone : ViewStates.Visible;
                _taskDueDateConteiner.Visibility = task.DueDate==DateTime.MinValue? ViewStates.Gone : ViewStates.Visible;
                _taskRemindDateConteiner.Visibility = task.RemindDate == DateTime.MinValue ? ViewStates.Gone : ViewStates.Visible;

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
            var task = _viewModel.GetItem(_viewModel.Id);
            if (task.IsSolved)
            {
                _viewModel.ChangeStatus(task);      
            }
            else
            {
                _viewModel.ChangeStatus(task);
            }
        }
        private void OnDeleteClick()
        {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(GetString(Resource.String.confirm_delete_task));
            alert.SetPositiveButton(GetString(Resource.String.dialog_yes), (senderAlert, args) =>
            {
                _viewModel.DeleteItem(_viewModel.Id);
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