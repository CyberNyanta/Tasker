using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.App.AlertDialog;
using Java.Util;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.AL.Utils;
using Tasker.Core.DAL.Entities;
using Tasker.Core;
using Tasker.Droid.Adapters;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using Tasker.Droid.AL.Utils;
using Tasker.Core.AL.Utils.Contracts;

namespace Tasker.Droid.Activities
{
    [Activity(LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class TaskEditCreateActivity : AppCompatActivity
    {
        #region Props
        private ITaskDetailsViewModel _viewModel;
        private INotificationUtils _notificationUtils;
        private List<Project> _projects;
        #region Views
        private EditText _taskTitle;
        private EditText _taskDescription;
        private TextView _taskDueDate;
        private TextView _taskRemindDate;
        private TextView _taskProject;
        private LinearLayout _colorContainer;
        private ImageView _colorShape;
        private TextView _colorName;
        #endregion
        private DateTime _dueDate = DateTime.MaxValue;
        private DateTime _remindDate = DateTime.MaxValue;
        private TaskColors _taskColor;
        private int _projectId;
        private bool _is24hoursFormat;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.task_edit_create);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.Title = GetString(Resource.String.task_edit_title);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            _viewModel = TinyIoCContainer.Current.Resolve<ITaskDetailsViewModel>();
            _notificationUtils = TinyIoCContainer.Current.Resolve<INotificationUtils>();
            _taskTitle = FindViewById<EditText>(Resource.Id.task_title);
            _taskDescription = FindViewById<EditText>(Resource.Id.task_description);
            _taskDueDate = FindViewById<TextView>(Resource.Id.task_dueDate);
            _taskRemindDate = FindViewById<TextView>(Resource.Id.task_remindDate);
            _taskProject = FindViewById<TextView>(Resource.Id.task_project);
            _colorContainer = FindViewById<LinearLayout>(Resource.Id.color_container);
            _colorShape = FindViewById<ImageView>(Resource.Id.color_shape);
            _colorName = FindViewById<TextView>(Resource.Id.color_name);

            _is24hoursFormat = GetSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private)
                .GetBoolean(GetString(Resource.String.settings_24hours_format), false);
            _projects = _viewModel.GetProjects();
            _projects.Insert(0, new Project
            {
                Title = ApplicationContext.GetString(Resource.String.project_inbox)
            });

            _taskDueDate.Click += delegate (Object o, EventArgs a) { SetDueDate(); };
            _taskRemindDate.Click += delegate (Object o, EventArgs a) { SetRemindDate(); };
            _taskProject.Click += delegate (Object o, EventArgs a) { SetProject(); };
            _colorContainer.Click += delegate (Object o, EventArgs a) { SetColor(); };
            _colorShape.Click += delegate (Object o, EventArgs a) { SetColor(); };
            _colorName.Click += delegate (Object o, EventArgs a) { SetColor(); };

            Initialization();
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
                case Resource.Id.menu_delete:
                    OnDeleteClick();
                    break;
                case Resource.Id.menu_complete:
                    item.SetTitle(_viewModel.GetItem().IsSolved ? Resource.String.complete_task : Resource.String.uncomplete_task);
                    _viewModel.ChangeStatus(_viewModel.Id);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (_viewModel.Id != 0)
            {
                MenuInflater.Inflate(Resource.Menu.task_edit_menu, menu);
                var item = menu.FindItem(Resource.Id.menu_complete);
                item.SetTitle(_viewModel.GetItem().IsSolved ? Resource.String.uncomplete_task : Resource.String.complete_task);
            }
            else
            {
                MenuInflater.Inflate(Resource.Menu.task_create_menu, menu);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        private void Initialization()
        {
            _viewModel.Id = Intent.GetIntExtra(IntentExtraConstants.TASK_ID_EXTRA, 0);


            Task task = null;
            if (_viewModel.Id != 0)
                task = _viewModel.GetItem();

            if (task != null)
            {
                _taskTitle.Text = task.Title;
                _taskDescription.Text = task.Description;
                if (task.DueDate != DateTime.MaxValue)
                {
                    _dueDate = task.DueDate;
                    _taskDueDate.Text = DateTimeConverter.DueDateToString(_dueDate);
                }
                _remindDate = task.RemindDate;
                InitRemindDate();
                var project = _projects.Find(x => x.ID == task.ProjectID);
                _taskProject.Text = project.Title;
                _taskProject.Tag = project.ID;
                _projectId = project.ID;
                _taskColor = task.Color;
            }
            else
            {
                _projectId = Intent.GetIntExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                _taskColor = default(TaskColors);
                var project = _projects.Find(x => x.ID == _projectId);
                if (project != null)
                {
                    _taskProject.Text = project.Title;
                    _taskProject.Tag = project.ID;

                }
                SetDueDate((TaskDueDates)Intent.GetIntExtra(IntentExtraConstants.DUE_DATE_TYPE_EXTRA, 4));
            }

            _colorName.Text = _taskColor.ToString();
            GradientDrawable drawable = (GradientDrawable)_colorShape.Drawable;
            drawable.Mutate().SetColorFilter(Color.ParseColor(TaskConstants.Colors[_taskColor]), PorterDuff.Mode.Src);
        }

        private void InitRemindDate()
        {
            if (_remindDate != DateTime.MaxValue && _remindDate > DateTime.Now)
            {
                if (_dueDate == DateTime.MaxValue)
                {
                    _taskRemindDate.Text = _remindDate.ToString(GetString(Resource.String.datetime_regex));
                }
                else
                {
                    var odd = (int)(_dueDate - _remindDate).TotalMinutes;
                    switch (odd)
                    {
                        case 15:
                            _taskRemindDate.Text = GetString(Resource.String.remind_dates_in15Minutes);
                            break;
                        case 30:
                            _taskRemindDate.Text = GetString(Resource.String.remind_dates_in30Minutes);
                            break;
                        case 60:
                            _taskRemindDate.Text = GetString(Resource.String.remind_dates_in1Hour);
                            break;
                        default:
                            _taskRemindDate.Text = _remindDate.ToString(GetString(Resource.String.datetime_regex));
                            break;
                    }
                }
            }
            else
            {
                _remindDate = DateTime.MaxValue;
                _taskRemindDate.Text = "";

            }
        }

        private void OnDeleteClick()
        {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Resource.String.confirm_delete_task)
                 .SetIcon(Resource.Drawable.ic_delete)
                 .SetPositiveButton(Resource.String.dialog_yes, (senderAlert, args) =>
                    {
                        var task = _viewModel.GetItem();
                        task.RemindDate = DateTime.MaxValue;
                        _notificationUtils.RemoveTaskReminder(task);
                        _viewModel.DeleteItem(_viewModel.Id);
                        SetResult(Result.Ok);
                        Finish();
                    })
                 .SetCancelable(true)
                 .SetNegativeButton(Resource.String.dialog_cancel, (senderAlert, args) => { })
                 .Show();
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
            if (error) return;
            var task = new Task()
            {
                ID = _viewModel.Id,
                Title = title,
                Description = desciption,
                Color = _taskColor,
                DueDate = _dueDate,
                RemindDate = _remindDate,
                ProjectID = _projectId
            };

            task.ID = _viewModel.SaveItem(task);
            _notificationUtils.SetTaskReminder(task);
            Finish();
        }

        #region Dialogs   

        #region ProjectDialog
        private void SetProject()
        {
            AlertDialog dialog = null;
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            dialog = alert.SetAdapter(new ProjectListDialogAdapter(this, _projects, _projectId, () => OnSetProject(dialog.Dismiss)), default(IDialogInterfaceOnClickListener))
                          .SetCancelable(true)
                          .Show();
        }

        private void OnSetProject(Action callback)
        {
            callback?.Invoke();
            _projectId = Intent.GetIntExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
            _taskProject.Text = _projects.Find(x => x.ID == _projectId).Title;
        }
        #endregion

        #region ColorDialog
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
            _taskColor = (TaskColors)Intent.GetIntExtra(IntentExtraConstants.TASK_COLOR_EXTRA, 0);
            _colorName.Text = _taskColor.ToString();
            GradientDrawable drawable = (GradientDrawable)_colorShape.Drawable;
            drawable.Mutate().SetColorFilter(Color.ParseColor(TaskConstants.Colors[_taskColor]), PorterDuff.Mode.Src);

        }
        #endregion

        #region DueDateDialog
        private void SetDueDate()
        {
            AlertDialog dialog = null;
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            dialog = builder.SetCancelable(true)
                            .SetAdapter(new DueDateListAdapter(this, _dueDate, (sender, args) =>
                            {
                                dialog.Dismiss();
                                SetDueDate((TaskDueDates)(int)((View)sender).Tag);
                                InitRemindDate();
                            }), default(IDialogInterfaceOnClickListener))
                            .Show();
        }

        private void SetDueDate(TaskDueDates type)
        {
            switch (type)
            {
                case TaskDueDates.Today:
                    _dueDate = DateTime.Today;
                    _taskDueDate.Text = GetString(Resource.String.due_dates_today);
                    break;
                case TaskDueDates.Tomorrow:
                    _dueDate = DateTime.Today.AddDays(1);
                    _taskDueDate.Text = GetString(Resource.String.due_dates_tomorrow);
                    break;
                case TaskDueDates.NextWeek:
                    _dueDate = DateTime.Today.AddDays(7);
                    _taskDueDate.Text = _dueDate.ToString(GetString(Resource.String.datetime_regex));
                    break;
                case TaskDueDates.Remove:
                    _dueDate = DateTime.MaxValue;
                    _taskDueDate.Text = "";
                    break;
                case TaskDueDates.PickDataTime:
                    var dateTimePicker = new SlideDateTimePicker.Builder(SupportFragmentManager);
                    var date = DateTime.UtcNow;
                    date.AddSeconds(-date.Second);
                    dateTimePicker.SetInitialDate(new Date(date.ToUnixTime()))
                                  .SetMinDate(new Date())
                                  .SetListener(new DueDateListener(this))
                                  .SetTheme(0)
                                  .SetIs24HourTime(_is24hoursFormat)
                                  .Build()
                                  .Show();
                    break;
            }
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
                _activity._taskDueDate.Text = DateTimeConverter.DueDateToString(_activity._dueDate);
                _activity.InitRemindDate();
            }
        }
        #endregion

        #region RemindDateDialog
        private void SetRemindDate()
        {
            if (_dueDate != DateTime.MaxValue && _dueDate.TimeOfDay != TimeSpan.FromTicks(0))
            {
                AlertDialog dialog = null;
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                dialog = builder.SetCancelable(true)
                                .SetAdapter(new RemindDateListAdapter(this, _remindDate, _dueDate, (sender, args) =>
                                {
                                    dialog.Dismiss();
                                    var selected = (TaskRemindDates)(int)((View)sender).Tag;
                                    switch (selected)
                                    {
                                        case TaskRemindDates.In15Minutes:
                                            _remindDate = _dueDate.AddMinutes(-15);
                                            _taskRemindDate.Text = GetString(Resource.String.remind_dates_in15Minutes);
                                            break;
                                        case TaskRemindDates.In30Minutes:
                                            _remindDate = _dueDate.AddMinutes(-30);
                                            _taskRemindDate.Text = GetString(Resource.String.remind_dates_in30Minutes);
                                            break;
                                        case TaskRemindDates.In1Hour:
                                            _remindDate = _dueDate.AddHours(-1);
                                            _taskRemindDate.Text = GetString(Resource.String.remind_dates_in1Hour);
                                            break;
                                        case TaskRemindDates.Remove:
                                            _remindDate = DateTime.MaxValue;
                                            _taskRemindDate.Text = "";
                                            break;
                                        case TaskRemindDates.PickDataTime:
                                            ShowRemindDateTimePicker();
                                            break;
                                    }

                                }), default(IDialogInterfaceOnClickListener))
                                .Show();
            }
            else
            {
                ShowRemindDateTimePicker();
            }

        }

        private void ShowRemindDateTimePicker()
        {
            var dateTimePicker = new SlideDateTimePicker.Builder(SupportFragmentManager);
            var date = DateTime.UtcNow;
            date = date.AddSeconds(-date.Second);
            dateTimePicker.SetInitialDate(new Date(date.ToUnixTime()))
                          .SetMinDate(new Date())
                          .SetListener(new RemindDateListener(this))
                          .SetTheme(0)
                          .SetIs24HourTime(_is24hoursFormat)
                          .Build()
                          .Show();
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
                if (_activity._remindDate < DateTime.Now)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(_activity);
                    alert.SetCancelable(true)
                         .SetTitle(Resource.String.remind_error)
                         .SetIcon(Resource.Drawable.ic_remind)
                         .SetNegativeButton(Resource.String.dialog_cancel, (senderAlert, args) => { })
                         .Show();
                }
                _activity.InitRemindDate();
            }
        }
        #endregion

        #endregion
    }
}