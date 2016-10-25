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

namespace Tasker.Droid.Adapters
{
    public class ColorListAdapter : BaseAdapter<TaskColors>
    {
        private Activity _context;
        private List<TaskColors> _colors;
        private TaskColors _current;

        public ColorListAdapter(Activity context, TaskColors current) : base()
        {
            _context = context;
            _current = current;
            _colors = Enum.GetValues(typeof(TaskColors)).Cast<TaskColors>().ToList();
        }

        public override TaskColors this[int position]
        {
            get { return _colors[position]; }
        }

        public override int Count
        {
            get { return _colors.Count; }
        }

        public override long GetItemId(int position)
        {
            return (long)_colors[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            var item = _colors[position];
            View view;
            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            if (convertView == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.color_list_item, null);
            }
            else
            {
                view = convertView;
            }
            if (item ==_current)
            {
                view.SetBackgroundResource(Resource.Color.item_selected);
            }
           
            var colorDrawable = view.FindViewById<ImageView>(Resource.Id.color_shape);
            var colorName = view.FindViewById<TextView>(Resource.Id.color_name);

            GradientDrawable drawable = (GradientDrawable)colorDrawable.Drawable;
            drawable.Mutate().SetColorFilter(Color.ParseColor(TaskConstants.Colors[item]),PorterDuff.Mode.Src);

            colorName.Text = item.ToString();
            //Finally return the view
            return view;
        }

    }
}