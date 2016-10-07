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

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.AL.Utils;
using Tasker.Core.DAL.Entities;
using Tasker.Core;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using Java.Util;
using Android.Graphics;

namespace Tasker.Droid.Activities
{
    [Activity]
    public class TaskEditCreateActivity: AppCompatActivity
    {
        private ITaskDetailsViewModel _viewModel;
        private EditText _taskTitle;
        private EditText _taskDescription;
        private TextView _taskDueDate;
        private TextView _taskRemindDate;
        private List<RadioButton> _taskColors = new List<RadioButton>();



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.task_edit_create);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Task Edit";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            _viewModel = TinyIoCContainer.Current.Resolve<ITaskDetailsViewModel>();

            _taskTitle = FindViewById<EditText>(Resource.Id.task_title);
            _taskDescription = FindViewById<EditText>(Resource.Id.task_description);
            _taskDueDate = FindViewById<TextView>(Resource.Id.task_dueDate);
            _taskRemindDate = FindViewById<TextView>(Resource.Id.task_remindDate);
            _taskColors.Add(FindViewById<RadioButton>(Resource.Id.color_none));
            _taskColors.Add(FindViewById<RadioButton>(Resource.Id.color_1));
            _taskColors.Add(FindViewById<RadioButton>(Resource.Id.color_2));
            _taskColors.Add(FindViewById<RadioButton>(Resource.Id.color_3));

            _taskDueDate.Click += delegate
            {
                var dateTimePicker = new SlideDateTimePicker.Builder(SupportFragmentManager);
                dateTimePicker.SetInitialDate(new Date());
                dateTimePicker.SetMinDate(new Date());
                dateTimePicker.SetListener(new CustomSlideDateTimeListener(this));
                dateTimePicker.SetTheme(0);
                var dialog = dateTimePicker.Build();
                dialog.Show();
            };

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                case Resource.Id.menu_save:
                    OnSaveClick();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
      
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.task_edit_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void OnSaveClick()
        {
            var error = false;
            var title = _taskTitle.Text;
            var desciption = _taskDescription.Text;
            if (!title.IsLengthInRange(TaskConstants.TASK_TITLE_MAX_LENGTH,1))
            {
                _taskTitle.Error = GetString(Resource.String.title_error);
                error = true;
            }
            if (!desciption.IsLengthInRange(TaskConstants.TASK_DESCRIPTION_MAX_LENGTH))
            {
                _taskDescription.Error = GetString(Resource.String.decription_error);
                error = true;
            }
            if (error) return;
            var task = new Task() { Title = title, Description = desciption, DueDate = DateTime.Now};

            _viewModel.SaveItem(task);
            SetResult(Result.Ok);
            Finish();
        }

        public class CustomSlideDateTimeListener : SlideDateTimeListener
        {
            TaskEditCreateActivity _activity;
            public CustomSlideDateTimeListener(TaskEditCreateActivity activity) : base()
            {
                _activity = activity;
            }
            public override void OnDateTimeSet(Date p0)
            {
                _activity._taskDueDate.Text = p0.ToString();
                Toast.MakeText(_activity, p0.ToString(), ToastLength.Long).Show();
            }

            public void onDateTimeCancel()
            {
                Toast.MakeText(_activity, "Canceled", ToastLength.Long).Show();
            }
        }
    }
}