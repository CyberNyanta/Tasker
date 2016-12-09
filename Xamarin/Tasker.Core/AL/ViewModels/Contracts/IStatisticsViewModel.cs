using System.Collections.Generic;

namespace Tasker.Core.AL.ViewModels.Contracts
{
    public interface IStatisticsViewModel
    {
        int[] GetWeeklyCompleteTaskStatistics();

        int[] GetMonthlyCompleteTaskStatistics();

        KeyValuePair<int, int> GetCompleteToOpenTaskStatistics();

        Dictionary<int, int> GetProjectsOpenTaskStatistics();

        Dictionary<int, int> GetProjectsCompleteTaskStatistics();
    }
}
