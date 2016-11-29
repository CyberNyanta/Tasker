using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Core.BL.Contracts;

namespace Tasker.Core.BL
{
    public class StatisticsProvider : IStatisticsProvider
    {
        private ITaskManager _taskManager;

        public StatisticsProvider(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }
        public int[] GetCompleteTaskStatisticsOnRange(int daysCount)
        {
            var tasks = _taskManager.GetAllCompleted().FindAll(task => task.CompletedDate > DateTime.Today.AddDays(-daysCount));
            var array = new int[daysCount];
            for (int i = daysCount; i > 0; i--)
            {
                array[daysCount - i] = tasks.FindAll(task => task.CompletedDate.Day == DateTime.Today.AddDays(-i+1).Day).Count;
            }
            return array;
        }
        public int[] GetMonthlyCompleteTaskStatistics()
        {
            return GetCompleteTaskStatisticsOnRange(30);
        }

        public int[] GetWeeklyCompleteTaskStatistics()
        {
            return GetCompleteTaskStatisticsOnRange(7);
        }

        public Dictionary<int, int> GetProjectsCompleteTaskStatistics()
        {
            var projects = _taskManager.GetProjects();
            var dictionary = new Dictionary<int, int>();
            foreach (var project in projects)
            {
                dictionary.Add(project.ID, _taskManager.GetProjectCompletedTasks(project.ID).Count);
            }
            return dictionary;
        }

        public KeyValuePair<int, int> GetCompleteToOpenTaskStatistics()
        {
            return new KeyValuePair<int, int>(_taskManager.GetAllCompleted().Count, _taskManager.GetAllOpen().Count);
        }

        public Dictionary<int, int> GetProjectsOpenTaskStatistics()
        {
            var projects = _taskManager.GetProjects();
            var dictionary = new Dictionary<int, int>();
            foreach (var project in projects)
            {
                dictionary.Add(project.ID, _taskManager.GetProjectOpenTasks(project.ID).Count);
            }
            return dictionary;
        }

      
    }
}
