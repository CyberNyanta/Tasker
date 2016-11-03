using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Tasker.Core;
using Android.Graphics.Drawables;

namespace Tasker.Droid.Adapters
{
    public class ColorListAdapter : BaseAdapter<TaskColors>
    {
        private Activity _context;
        private List<TaskColors> _colors;
        private TaskColors _current;
        private event Action OnClick;

        public ColorListAdapter(Activity context, TaskColors current, Action callback) : base()
        {
            _context = context;
            _current = current;
            OnClick += callback;
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

            var item = _colors[position];
            View view;
            view = _context.LayoutInflater.Inflate(Resource.Layout.color_list_item, null);
            if (Enum.Equals(item,_current))
            {
                view.SetBackgroundResource(Resource.Color.item_selected);
            }

            var colorDrawable = view.FindViewById<ImageView>(Resource.Id.color_shape);
            var colorName = view.FindViewById<TextView>(Resource.Id.color_name);
            
            GradientDrawable drawable = (GradientDrawable)colorDrawable.Drawable;
            drawable.Mutate().SetColorFilter(Color.ParseColor(TaskConstants.Colors[item]),PorterDuff.Mode.Src);

            colorName.Text = item.ToString();

            colorDrawable.Click -= View_Click;
            colorName.Click -= View_Click;
            view.Click -= View_Click;
            view.Click += View_Click;
            colorDrawable.Click += View_Click;
            colorName.Click += View_Click;
            view.Tag = (int)item;
            colorDrawable.Tag = (int)item;
            colorName.Tag = (int)item;

            return view;
        }

        private void View_Click(object sender, EventArgs e)
        {
            _context.Intent.PutExtra(IntentExtraConstants.TASK_COLOR_EXTRA, (int)((View)sender).Tag);      

            _context.RunOnUiThread(() =>
            {
                OnClick?.Invoke();
            });
            
        }
    }
}