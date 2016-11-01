using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;
using Tasker.Core;

namespace Tasker.Droid.Adapters
{
    public class DueDateListAdapter : BaseAdapter<TaskDueDates>
    {
        private Activity _context;
        private List<TaskDueDates> _dates;
        private DateTime _current;
        private TaskDueDates _currentType;
        private event EventHandler OnClick;

        public DueDateListAdapter(Activity context, DateTime current, EventHandler callback) : base()
        {
            _context = context;
            _current = current;
            OnClick += callback;
            _dates = Enum.GetValues(typeof(TaskDueDates)).Cast<TaskDueDates>().ToList();
            if (_current.Date == DateTime.Today)
            {
                _currentType = TaskDueDates.Today;
            }
            else if (_current.Date == DateTime.Today.AddDays(1))
            {
                _currentType = TaskDueDates.Tomorrow;
            }
            else if (_current == DateTime.MaxValue)
            {
                _currentType = TaskDueDates.Remove;
            }
        }

        public override TaskDueDates this[int position]
        {
            get { return _dates[position]; }
        }

        public override int Count
        {
            get { return _dates.Count; }
        }

        public override long GetItemId(int position)
        {
            return (long)_dates[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var item = _dates[position];
            View view;

            view = _context.LayoutInflater.Inflate(Resource.Layout.date_list_item, null);

            var dateName = view.FindViewById<TextView>(Resource.Id.date_name);



            if (item == _currentType)
            {
                switch (item)
                {
                    case TaskDueDates.PickDataTime:
                        view.SetBackgroundResource(Resource.Color.item_selected);
                        dateName.Text = _current.ToString(_context.GetString(Resource.String.datetime_regex));
                        break;
                    case TaskDueDates.Today:
                        view.SetBackgroundResource(Resource.Color.item_selected);
                        if (_current != DateTime.Today)
                            dateName.Text = _context.GetString(Resource.String.due_dates_today_at, _current.ToString(_context.GetString(Resource.String.time_regex)));
                        else
                            dateName.Text = _context.GetString(Resource.String.due_dates_today);
                        break;
                    case TaskDueDates.Tomorrow:
                        view.SetBackgroundResource(Resource.Color.item_selected);
                        if (_current != DateTime.Today.AddDays(1))
                            dateName.Text = _context.GetString(Resource.String.due_dates_tomorrow_at, _current.ToString(_context.GetString(Resource.String.time_regex)));
                        else
                            dateName.Text = _context.GetString(Resource.String.due_dates_tomorrow);
                        break;
                    case TaskDueDates.Remove:
                        dateName.Text = _context.GetString(Resource.String.due_dates_remove);
                        break;
                }
            }
            else
            {
                switch (item)
                {
                    case TaskDueDates.PickDataTime:
                        dateName.Text = _context.GetString(Resource.String.due_dates_pick);
                        break;
                    case TaskDueDates.Today:
                        dateName.Text = _context.GetString(Resource.String.due_dates_today);
                        break;
                    case TaskDueDates.Tomorrow:
                        dateName.Text = _context.GetString(Resource.String.due_dates_tomorrow);
                        break;
                    case TaskDueDates.NextWeek:
                        dateName.Text = _context.GetString(Resource.String.due_dates_nextWeek);
                        break;
                    case TaskDueDates.Remove:
                        dateName.Text = _context.GetString(Resource.String.due_dates_remove);
                        break;
                }
            }
            view.Click += OnClick;
            dateName.Click += OnClick;
            view.Tag = (int)item;
            dateName.Tag = (int)item;

            return view;
        }
    }
}