﻿using System;
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

            {TaskColors.Tomato,"#FFFF6347" },
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
        Tomato,
        Yellow,
        Violet
    }
}
