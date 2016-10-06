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

        public static readonly string[] Colors =
        {
            "None",
            "#32FF0000",
            "#3200FF00",
            "#320000FF"
        };
    }

    public enum TaskColors
    {
        None,
        Red,
        Green,
        Blue,
        
    }
}
