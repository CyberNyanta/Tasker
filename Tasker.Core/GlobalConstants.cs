using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Core
{
    public static class TaskConstants
    {
        public const Int32 TASK_DESCRIPTION_MAX_LENGTH = 1000;

        public const Int32 TASK_TITLE_MAX_LENGTH = 100;

        public const Int32 PROJECT_TITLE_MAX_LENGTH = 50;

        public const float COMPLETED_TASK_BACKGROUND_ALPHA = 0.4f;
        public const float TASK_BACKGROUND_ALPHA = 1f;

        public static readonly Dictionary<TaskColors, string> Colors = new Dictionary<TaskColors, string>()
        {
            {TaskColors.None,"#FF9E9E9E" },
            {TaskColors.Lime,"#FF00FF00" },
            {TaskColors.Peach,"#FFFFDAB9" },
            {TaskColors.Aqua,"#FF00FFFF" },
            {TaskColors.Blue,"#FF87CEFA" },
            {TaskColors.Salmon,"#FFFA8072" },
            {TaskColors.Teal,"#FF008080" },
            {TaskColors.Tan,"#FFD2B48C" },
            {TaskColors.Yellow,"#FFFFFF00" },
            {TaskColors.Violet,"#FFEE82EE" }
        };

    }

    public enum TaskColors
    {
        None,
        Lime,
        Peach,
        Aqua,
        Blue,
        Salmon,
        Teal,
        Tan,
        Yellow,
        Violet
    }

    public enum TaskDueDates
    {
        PickDataTime,
        Today,
        Tomorrow,
        NextWeek,
        Remove,
    }

    public enum TaskRemindDates
    {
        PickDataTime,
        In15Minutes,
        In30Minutes,
        In1Hour,
        Remove,
    }

    public enum TaskListType
    {
        AllOpen,
        AllSolve,
        ProjectOpen,
        ProjectSolve,
        Today,
        Tomorrow,
        NextWeek,
    }

    public static class Extensions
    {    
        public static bool IsOpenType(this TaskListType type)
        {
            return type != TaskListType.ProjectSolve && type != TaskListType.AllSolve;
        }
        public static bool IsSolveType(this TaskListType type)
        {
            return type == TaskListType.AllSolve || type == TaskListType.ProjectSolve;
        }
    }
}
