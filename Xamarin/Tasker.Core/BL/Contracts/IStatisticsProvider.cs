using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Core.BL.Contracts
{
    public interface IStatisticsProvider
    {
        int[] GetWeeklyCompleteTaskStatistics();

        int[] GetMonthlyCompleteTaskStatistics();

        int[] GetCompleteTaskStatisticsOnRange(int daysCount);

        KeyValuePair<int, int> GetCompleteToOpenTaskStatistics();

        Dictionary<int,int> GetProjectsOpenTaskStatistics();

        Dictionary<int, int> GetProjectsCompleteTaskStatistics();
    }
}
