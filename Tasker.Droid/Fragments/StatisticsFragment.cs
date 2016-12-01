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
        private LineChartView _lineChart;
        private PieChartView _pieChart;
        private View _chartContainer;

        int[,] randomNumbersTab = new int[1, 5];
        
        private bool hasPoints = false;

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
            _lineChart = view.FindViewById<LineChartView>(Resource.Id.complete_chart);
            _pieChart = view.FindViewById<PieChartView>(Resource.Id.pie_chart);
            _chartContainer = view.FindViewById(Resource.Id.chart_container);
            _bottomBar = BottomBar.Attach(_chartContainer, savedInstanceState);
            _bottomBar.SetItems(Resource.Menu.statistics_bottom_bar_menu);
            _bottomBar.SetOnMenuTabClickListener(this);
            _bottomBar.MapColorForTab(0, "#5D4037");
            _bottomBar.MapColorForTab(1, "#5D4037");
            _bottomBar.MapColorForTab(2, "#7B1FA2");

        }

        private void SetWeekly()
        {
            _lineChart.Visibility = ViewStates.Visible;

            _pieChart.Visibility = ViewStates.Gone;
            List<Line> lines = new List<Line>();
            List<AxisValue> axisValues = new List<AxisValue>();
            for (int i = 0; i < 5; i++)
            {
                axisValues.Add(new AxisValue(i).SetLabel($"{DateTime.Today.AddDays(-6 + i).ToString("d MMM")}"));
            }
            axisValues.Add(new AxisValue(5).SetLabel($"Yesterday"));

            List<PointValue> values = new List<PointValue>();
            var weeklyStatistics = _viewModel.GetWeeklyCompleteTaskStatistics();
            for (int j = 0; j < weeklyStatistics.Length; j++)
            {
                values.Add(new PointValue(j, weeklyStatistics[j]));
            }

            Line line = new Line(values);
            line.SetColor(ChartUtils.ColorRed);
            line.SetHasPoints(false);
            line.SetFilled(true);

            lines.Add(line);

            var data = new LineChartData(lines);
            data.AxisXBottom = new Axis(axisValues).SetHasLines(true);
            data.AxisYLeft = (new Axis().SetHasLines(true).SetMaxLabelChars(3));

            data.SetBaseValue(float.NegativeInfinity);
            _lineChart.LineChartData = data;
        }

        private void SetMonthly()
        {
            _lineChart.Visibility = ViewStates.Visible;

            _pieChart.Visibility = ViewStates.Gone;
            List<Line> lines = new List<Line>();
            List<AxisValue> axisValues = new List<AxisValue>();
            for (int i = 0; i < 29; i++)
            {
               axisValues.Add(new AxisValue(i).SetLabel($"{DateTime.Today.AddDays(-29 + i).ToString("d MMM")}"));
            }
            List<PointValue> values = new List<PointValue>();
            var weeklyStatistics = _viewModel.GetMonthlyCompleteTaskStatistics();
            for (int j = 0; j < weeklyStatistics.Length; j++)
            {
                values.Add(new PointValue(j, weeklyStatistics[j]));
            }

            Line line = new Line(values);
            line.SetColor(ChartUtils.ColorRed);
            line.SetHasPoints(false);
            line.SetFilled(true);
            lines.Add(line);

            var data = new LineChartData(lines);
            data.AxisXBottom = new Axis(axisValues)
                .SetHasLines(true)
                .SetMaxLabelChars(6);
            data.AxisYLeft = (new Axis().SetHasLines(true).SetMaxLabelChars(3));

            data.SetBaseValue(float.NegativeInfinity);
            _lineChart.LineChartData = data;
        }

        private void SetOverall()
        {
            var chartData = new PieChartData();
            var values = new List<SliceValue>();
            var data = _viewModel.GetCompleteToOpenTaskStatistics();
            values.Add(new SliceValue(data.Key, ChartUtils.ColorOrange).SetLabel(Context.GetString(Resource.String.chart_completed,data.Key)));
            values.Add(new SliceValue(data.Value, ChartUtils.ColorGreen).SetLabel(Context.GetString(Resource.String.chart_open, data.Value)));
            chartData.SetValues(values);
            chartData.SetHasLabels(true);
            chartData.SetHasLabelsOutside(true);
            _lineChart.Visibility = ViewStates.Gone;        
            _pieChart.Visibility = ViewStates.Visible;
            _pieChart.PieChartData = chartData;
            _pieChart.CircleFillRatio =0.6f;
        }

        public void OnMenuTabSelected(int menuItemId)
        {

            switch (menuItemId)
            {
                case Resource.Id.menu_weekly_statistics:
                    SetWeekly();
                    break;
                case Resource.Id.menu_monthly_statistics:
                    SetMonthly();
                    break;
                case Resource.Id.menu_overall_statistics:
                    SetOverall();
                    break;
            }
        }

        public void OnMenuTabReSelected(int menuItemId)
        {

        }
    }
}