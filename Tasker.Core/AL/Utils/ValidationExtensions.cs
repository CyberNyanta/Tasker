using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Core.AL.Utils
{
    public static class ValidationExtensions
    {
        public static bool IsLengthLess(this string text, int maxLength)
        {
            return text.Length <= maxLength ? true : false;
        }

    }
}
