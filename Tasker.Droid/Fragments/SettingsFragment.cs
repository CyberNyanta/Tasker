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
using Android.Views.Animations;

using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;
using FAB = Clans.Fab.FloatingActionButton;
using Com.Wdullaer.Swipeactionadapter;

using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.DAL.Entities;
using Tasker.Core.AL.Utils;
using Tasker.Core;
using Tasker.Droid.Activities;
using Tasker.Droid.Adapters;

using Lecho.Lib.Hellocharts.View;
using Lecho.Lib.Hellocharts.Model;
using Lecho.Lib.Hellocharts.Util;
using Axis = Lecho.Lib.Hellocharts.Model.Axis;


namespace Tasker.Droid.Fragments
{
    public class SettingsFragment : Fragment
    {
        private Switch _pushNotificatin;
        private Switch _24hoursFormat;
        private View _startPage;
        private TextView _startPageTitle;
        private TextView _startPageCurrent;
        private ISharedPreferences _sharedPreferences;
        private IProjectListViewModel _viewModel;
        private StartScreens _startScreen;
        private string _startScreenName;
        private int _projectId =0;



        private LineChartView chart;
        private LineChartData data;
        private int numberOfLines = 1;
        private int numberOfPoints = 5;
        int[,] randomNumbersTab = new int[1, 5];

        private bool hasAxes = true;
        private bool hasAxesNames = true;
        private bool hasLines = true;
        private bool hasPoints = true;
        private ValueShape shape = ValueShape.Circle;
        private bool isFilled = false;
        private bool hasLabels = false;
        private bool isCubic = false;
        private bool hasLabelForSelected = false;
        private bool pointsHaveDifferentColor;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            return inflater.Inflate(Resource.Layout.settings, container, false);  
        }

        public override void OnPrepareOptionsMenu(IMenu menu)
        {
            menu.Clear();
            base.OnPrepareOptionsMenu(menu);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _sharedPreferences = Activity.GetSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private);
            _viewModel = TinyIoCContainer.Current.Resolve<IProjectListViewModel>();
            _pushNotificatin = view.FindViewById<Switch>(Resource.Id.setting_notification);
            _24hoursFormat = view.FindViewById<Switch>(Resource.Id.setting_24time);
            _startPage = view.FindViewById<View>(Resource.Id.setting_start_page);
            _startPageTitle = view.FindViewById<TextView>(Resource.Id.setting_start_page_title);
            _startPageCurrent = view.FindViewById<TextView>(Resource.Id.setting_start_page_current);

            _pushNotificatin.Checked = _sharedPreferences.GetBoolean(GetString(Resource.String.settings_push_notifications), _pushNotificatin.Checked);
            _24hoursFormat.Checked = _sharedPreferences.GetBoolean(GetString(Resource.String.settings_push_notifications), _24hoursFormat.Checked);
            _startScreenName = _sharedPreferences.GetString(GetString(Resource.String.settings_start_page_name), GetString(Resource.String.navigation_all));
            _startPageCurrent.Text = _startScreenName;
            _startPage.Click += (o, args)=>{ SetStartPage(); };
            chart = view.FindViewById<LineChartView>(Resource.Id.line_chart);
            generateData();
        }

        private void generateData()
        {

            List<Line> lines = new List<Line>();
            List<AxisValue> axisValues = new List<AxisValue>();
            axisValues.Add(new AxisValue(0).SetLabel($"# {0}"));
            axisValues.Add(new AxisValue(1).SetLabel($"# {1}"));
            for (int i = 0; i < numberOfLines; ++i)
            {

                List<PointValue> values = new List<PointValue>();
                for (int j = 0; j < numberOfPoints; ++j)
                {
                    values.Add(new PointValue(j, j));
                }

                Line line = new Line(values);
                line.SetColor(ChartUtils.Colors[i]);
                line.SetShape(shape);
                line.SetCubic(isCubic);
                line.SetFilled(isFilled);
                line.SetHasLabels(hasLabels);
                line.SetHasLabelsOnlyForSelected(hasLabelForSelected);
                line.SetHasLines(hasLines);
                line.SetHasPoints(hasPoints);
                if (pointsHaveDifferentColor)
                {
                    line.SetPointColor(ChartUtils.Colors[(i + 1) % ChartUtils.Colors.Count]);
                }
                lines.Add(line);
            }

            data = new LineChartData(lines);
            data.AxisXBottom = new Axis(axisValues).SetHasLines(true);
            data.AxisYLeft = (new Axis().SetHasLines(true).SetMaxLabelChars(3));
            

            data.SetBaseValue(float.NegativeInfinity);
            chart.LineChartData = data;

        }


        public override void OnStop()
        {
            _sharedPreferences.Edit()
                .PutBoolean(GetString(Resource.String.settings_push_notifications), _pushNotificatin.Checked)
                .PutBoolean(GetString(Resource.String.settings_24hours_format), _24hoursFormat.Checked)
                .PutInt(GetString(Resource.String.settings_start_page), (int)_startScreen)
                .PutString(GetString(Resource.String.settings_start_page_name), _startScreenName)
                .PutInt(GetString(Resource.String.project), _projectId)
                .Commit();
            base.OnDestroy();
        }

        private void SetStartPage()
        {
            Dialog dialog = null;
            var list = (ExpandableListView) Activity.LayoutInflater.Inflate(Resource.Layout.expandable_list, null);
            list.SetAdapter(new StartPageDialogAdapter(Activity, _viewModel.GetAll()));
            list.ChildClick += (sender, args) =>
            {
                dialog.Cancel();
                switch (args.GroupPosition)
                {
                    case 0:
                        _startScreen = (StartScreens)args.ChildPosition;
                        _startScreenName = _startScreen.ToLocalString();
                        _startPageCurrent.Text = _startScreenName;
                        break;
                    case 1:
                        _projectId = (int)args.Id;
                        _startScreen = StartScreens.SelectedProject;
                        _startScreenName = _viewModel.GetItem(_projectId).Title;
                        _startPageCurrent.Text = _startScreenName;                     
                        break;
                }
            };
            
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            dialog = alert.SetView(list)
                          .SetCancelable(true)
                          .Show();
        }    
    }
}