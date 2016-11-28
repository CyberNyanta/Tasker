using System;
using System.Collections.Generic;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.BL.Contracts;

namespace Tasker.Core.AL.ViewModels
{
    public class StatisticsViewModel : IStatisticsViewModel
    {
        private IStatisticsProvider _statisticProvider;
        public StatisticsViewModel(IStatisticsProvider statisticProvider)
        {
            _statisticProvider = statisticProvider;
        }       

        public KeyValuePair<int, int> GetCompleteToOpenTaskStatistics()
        {
            return _statisticProvider.GetCompleteToOpenTaskStatistics();
        }

        public int[] GetMonthlyCompleteTaskStatistics()
        {
            return _statisticProvider.GetMonthlyCompleteTaskStatistics();
        }

        public Dictionary<int, int> GetProjectsCompleteTaskStatistics()
        {
            return _statisticProvider.GetProjectsCompleteTaskStatistics();
        }

        public Dictionary<int, int> GetProjectsOpenTaskStatistics()
        {
            return _statisticProvider.GetProjectsOpenTaskStatistics();
        }

        public int[] GetWeeklyCompleteTaskStatistics()
        {
            return _statisticProvider.GetWeeklyCompleteTaskStatistics();
        }
    }
}
