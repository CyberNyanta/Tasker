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
using Android.Graphics;

using Tasker.Core.DAL.Entities;
using Tasker.Core;
using Tasker.Core.BL.Contracts;
using Android.Graphics.Drawables;
using Tasker.Droid.AL.Utils;

namespace Tasker.Droid.Adapters
{
    public class RemindDateListAdapter : BaseAdapter<TaskRemindDates>
    {
        private Activity _context;
        private List<TaskRemindDates> _dates;
        private DateTime _current;
        private TaskRemindDates _currentType;
        private event EventHandler OnClick;

        public RemindDateListAdapter(Activity context, DateTime current, DateTime dueDate, EventHandler callback) : base()
        {
            _context = context;
            _current = current;
            OnClick += callback;
            _dates = Enum.GetValues(typeof(TaskRemindDates)).Cast<TaskRemindDates>().ToList();
            var inm = (dueDate - _current).TotalMinutes;
            if (inm == 15)
            {
                _currentType = TaskRemindDates.In15Minutes;
            }
            else if(inm == 30)
            {
                _currentType = TaskRemindDates.In30Minutes;
            }
            else if (inm == 60)
            {
                _currentType = TaskRemindDates.In1Hour;
            }
            else if(current == DateTime.MaxValue)
            {
                _currentType = TaskRemindDates.Remove;
            }
        }

        public override TaskRemindDates this[int position]
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

            

            if (item == _currentType && item != TaskRemindDates.Remove)
            {
                view.SetBackgroundResource(Resource.Color.item_selected);
            }

            switch (item)
            {
                case TaskRemindDates.PickDataTime:                    
                    if(_currentType==item)
                        dateName.Text = DateTimeConverter.DateToString( _current);
                    else
                        dateName.Text = _context.GetString(Resource.String.remind_dates_pick);
                    break;
                case TaskRemindDates.In15Minutes:                  
                    dateName.Text = _context.GetString(Resource.String.remind_dates_in15Minutes);
                    break;
                case TaskRemindDates.In30Minutes:                    
                    dateName.Text = _context.GetString(Resource.String.remind_dates_in30Minutes);
                    break;
                case TaskRemindDates.In1Hour:                   
                    dateName.Text = _context.GetString(Resource.String.remind_dates_in1Hour);
                    break;
                case TaskRemindDates.Remove:
                    dateName.Text = _context.GetString(Resource.String.due_dates_remove);
                    break;
            }
            view.Click += OnClick;
            dateName.Click += OnClick;
            view.Tag = (int)item;
            dateName.Tag = (int)item;

            return view;
        }
    }
}