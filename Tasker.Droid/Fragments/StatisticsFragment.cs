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
using Tasker.Core.AL.ViewModels;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;

namespace Tasker.Droid.Fragments
{
    public class StatisticsFragment : Fragment, IOnMenuTabClickListener
    {
        private IStatisticsViewModel _viewModel;
        private BottomBar _bottomBar;
        private LineChartView _weekComplete;

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
        private bool hasLabelForSelected = true;
        private bool pointsHaveDifferentColor;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            return inflater.Inflate(Resource.Layout.statistic, container, false);
        }

        public override void OnPrepareOptionsMenu(IMenu menu)
        {
            menu.Clear();
            base.OnPrepareOptionsMenu(menu);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _viewModel = TinyIoCContainer.Current.Resolve<IStatisticsViewModel>();
            _weekComplete = view.FindViewById<LineChartView>(Resource.Id.complete_chart);
            _bottomBar = BottomBar.Attach(view.FindViewById(Resource.Id.chart_container), savedInstanceState);
            _bottomBar.SetItems(Resource.Menu.statistics_bottom_bar_menu);
            _bottomBar.MapColorForTab(0, "#5D4037");
            _bottomBar.MapColorForTab(1, "#5D4037");
            _bottomBar.MapColorForTab(2, "#7B1FA2");


            SetWeekly();
        }

        private void SetWeekly()
        {

            List<Line> lines = new List<Line>();
            List<AxisValue> axisValues = new List<AxisValue>();
            for (int i = 0; i < 5; i++)
            {
                axisValues.Add(new AxisValue(i).SetLabel($"{DateTime.Today.AddDays(-6+i).ToString("d MMM")}"));
            }
            axisValues.Add(new AxisValue(5).SetLabel($"Yesterday"));
            for (int i = 0; i < numberOfLines; ++i)
            {

                List<PointValue> values = new List<PointValue>();
                var weeklyStatistics = _viewModel.GetWeeklyCompleteTaskStatistics();
                for (int j = 0; j < weeklyStatistics.Length; j++)
                {
                    values.Add(new PointValue(j, weeklyStatistics[j]));
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
                line.SetFilled(true);
                if (pointsHaveDifferentColor)
                {
                    line.SetPointColor(ChartUtils.Colors[(i + 1) % ChartUtils.Colors.Count]);
                }
                lines.Add(line);
            }

            var data = new LineChartData(lines);
            data.AxisXBottom = new Axis(axisValues).SetHasLines(true);
            data.AxisYLeft = (new Axis().SetHasLines(true).SetMaxLabelChars(3));

            data.SetBaseValue(float.NegativeInfinity);
            _weekComplete.LineChartData = data;
            
        }

        public void OnMenuTabSelected(int menuItemId)
        {
            throw new NotImplementedException();
        }

        public void OnMenuTabReSelected(int menuItemId)
        {
            throw new NotImplementedException();
        }
    }
}