using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.App.AlertDialog;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.AL.Utils;
using Tasker.Core.DAL.Entities;
using Tasker.Core;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using Java.Util;
using Android.Graphics;
using Android.Graphics.Drawables;
using Tasker.Droid.Adapters;

namespace Tasker.Droid.Activities
{
    [Activity]
    public class TaskEditCreateActivity : AppCompatActivity
    {
        private ITaskDetailsViewModel _viewModel;
        private List<Project> _projects;
        private EditText _taskTitle;
        private EditText _taskDescription;
        private TextView _taskDueDate;
        private TextView _taskRemindDate;
        private TextView _taskProject;
        private LinearLayout _colorContainer;
        private ImageView _colorShape;
        private TextView _colorName;
        private DateTime _dueDate = DateTime.MaxValue;
        private DateTime _remindDate = DateTime.MaxValue;
        private TaskColors _taskColor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.task_edit_create);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.Title = GetString(Resource.String.task_edit_title);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            _viewModel = TinyIoCContainer.Current.Resolve<ITaskDetailsViewModel>();

            _taskTitle = FindViewById<EditText>(Resource.Id.task_title);
            _taskDescription = FindViewById<EditText>(Resource.Id.task_description);
            _taskDueDate = FindViewById<TextView>(Resource.Id.task_dueDate);
            _taskRemindDate = FindViewById<TextView>(Resource.Id.task_remindDate);
            _taskProject = FindViewById<TextView>(Resource.Id.task_project);
            _colorContainer = FindViewById<LinearLayout>(Resource.Id.color_container);
            _colorShape = FindViewById<ImageView>(Resource.Id.color_shape);
            _colorName = FindViewById<TextView>(Resource.Id.color_name);

            _projects = _viewModel.GetProjects();
            _projects.Insert(0, new Project
            {
                Title = ApplicationContext.GetString(Resource.String.project_inbox)
            });

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                for (int i = 0; i < 4; i++)
                {
                    //_taskColors[i].ButtonTintList = Android.Content.Res.ColorStateList.ValueOf(Color.ParseColor(TaskConstants.Colors[i]));
                }

            _taskDueDate.OnFocusChangeListener = new OnFocusChangeListener(SetDueDate);
            _taskRemindDate.OnFocusChangeListener = new OnFocusChangeListener(SetRemindDate);
            _taskProject.OnFocusChangeListener = new OnFocusChangeListener(SetProject);
            _taskDueDate.Click += delegate (Object o, EventArgs a) { SetDueDate(); };
            _taskRemindDate.Click += delegate (Object o, EventArgs a) { SetRemindDate(); };
            _taskProject.Click += delegate (Object o, EventArgs a) { SetProject(); };
            _colorContainer.Click += delegate (Object o, EventArgs a) { SetColor(); };
            _colorShape.Click += delegate (Object o, EventArgs a) { SetColor(); };
            _colorName.Click += delegate (Object o, EventArgs a) { SetColor(); };
            _viewModel.Id = Intent.GetIntExtra("TaskId", 0);

            Initialization();


        }

        private void Initialization()
        {
            Task task = null;
            if (_viewModel.Id != 0)
                task = _viewModel.GetItem(_viewModel.Id);

            if (task != null)
            {
                _taskTitle.Text = task.Title;
                _taskDescription.Text = task.Description;
                if (task.DueDate != DateTime.MaxValue)
                {
                    _dueDate = task.DueDate;
                    _remindDate = task.RemindDate;
                    _taskDueDate.Text = _dueDate.ToString(GetString(Resource.String.datetime_regex));
                }
                if (task.RemindDate != DateTime.MaxValue)
                {
                    _remindDate = task.RemindDate;
                    _taskRemindDate.Text = _remindDate.ToString(GetString(Resource.String.datetime_regex));
                }
                var project = _projects.Find(x => x.ID == task.ProjectID);
                _taskProject.Text = project.Title;
                _taskProject.Tag = project.ID;

                _taskColor = task.Color;
            }
            else
            {
                _taskColor = default(TaskColors);
            }


            _colorName.Text = _taskColor.ToString();
            GradientDrawable drawable = (GradientDrawable)_colorShape.Drawable;
            drawable.Mutate().SetColorFilter(Color.ParseColor(TaskConstants.Colors[_taskColor]), PorterDuff.Mode.Src);
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
            if (!title.IsLengthInRange(TaskConstants.TASK_TITLE_MAX_LENGTH, 1))
            {
                _taskTitle.Error = GetString(Resource.String.title_error);
                error = true;
            }
            if (!desciption.IsLengthInRange(TaskConstants.TASK_DESCRIPTION_MAX_LENGTH))
            {
                _taskDescription.Error = GetString(Resource.String.description_error);
                error = true;
            }

            if (error) return;

            var task = new Task()
            {
                ID = _viewModel.Id,
                Title = title,
                Description = desciption,
                Color = _taskColor,
                DueDate = _dueDate,
                RemindDate = _remindDate,
                ProjectID = (int)_taskProject.Tag
            };

            _viewModel.SaveItem(task);
            Finish();
        }

        private void SetDueDate()
        {
            var dateTimePicker = new SlideDateTimePicker.Builder(SupportFragmentManager);
            dateTimePicker.SetInitialDate(new Date());
            dateTimePicker.SetMinDate(new Date());
            dateTimePicker.SetListener(new DueDateListener(this));
            dateTimePicker.SetTheme(0);
            var dialog = dateTimePicker.Build();
            dialog.Show();
        }
        private void SetRemindDate()
        {
            var dateTimePicker = new SlideDateTimePicker.Builder(SupportFragmentManager);
            dateTimePicker.SetInitialDate(new Date());
            if (_dueDate != DateTime.MinValue)
            {
                dateTimePicker.SetMaxDate(new Date(_dueDate.ToUnixTime()));
            }
            dateTimePicker.SetMinDate(new Date());
            dateTimePicker.SetListener(new RemindDateListener(this));
            dateTimePicker.SetTheme(0);
            var dialog = dateTimePicker.Build();
            dialog.Show();
        }

        private void SetProject()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            //alert.SetTitle(GetString(Resource.String.project_create_dialog));
            alert.SetItems(
               _projects.Select(x => x.Title).ToArray(),
                delegate (object obj, DialogClickEventArgs args)
                {
                    var project = _projects[args.Which];
                    _taskProject.Text = project.Title;
                    _taskProject.Tag = project.ID;
                }
                        );
            alert.SetCancelable(true);

            alert.Show();
        }

        private void SetColor()
        {
            AlertDialog dialog = null;
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            dialog = alert.SetCancelable(true)
                          .SetAdapter(new ColorListAdapter(this, _taskColor, () => OnSetColor(dialog.Dismiss)), default(IDialogInterfaceOnClickListener))
                          .Show();
        }

        private void OnSetColor(Action callback)
        {
            callback?.Invoke();
            _taskColor = (TaskColors)Intent.GetIntExtra("TaskColor", 0);
            _colorName.Text = _taskColor.ToString();
            GradientDrawable drawable = (GradientDrawable)_colorShape.Drawable;
            drawable.Mutate().SetColorFilter(Color.ParseColor(TaskConstants.Colors[_taskColor]), PorterDuff.Mode.Src);

        }

        public class DueDateListener : SlideDateTimeListener
        {
            TaskEditCreateActivity _activity;
            public DueDateListener(TaskEditCreateActivity activity) : base()
            {
                _activity = activity;
            }
            public override void OnDateTimeSet(Date p0)
            {
                _activity._dueDate = p0.Time.UnixTimeToDateTime();
                _activity._taskDueDate.Text = _activity._dueDate.ToString(_activity.GetString(Resource.String.datetime_regex));
            }
        }

        public class RemindDateListener : SlideDateTimeListener
        {
            TaskEditCreateActivity _activity;
            public RemindDateListener(TaskEditCreateActivity activity) : base()
            {
                _activity = activity;
            }
            public override void OnDateTimeSet(Date p0)
            {
                _activity._remindDate = p0.Time.UnixTimeToDateTime();
                _activity._taskRemindDate.Text = _activity._remindDate.ToString(_activity.GetString(Resource.String.datetime_regex));
            }
        }

        public class OnFocusChangeListener : Java.Lang.Object, View.IOnFocusChangeListener
        {
            private event Action OnFocus;
            public OnFocusChangeListener(Action callback)
            {
                OnFocus += callback;

            }
            public void OnFocusChange(View v, bool hasFocus)
            {
                if (hasFocus)
                {
                    OnFocus?.Invoke();
                }
            }
        }
    }
}