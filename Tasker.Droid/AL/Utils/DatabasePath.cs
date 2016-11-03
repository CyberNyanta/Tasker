using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tasker.Core.DL;

namespace Tasker.Droid.AL.Utils
{
    class DatabasePath : IDatabasePath
    {
        public string GetDatabasePath()
        {
            var sqliteFilename = Constans.DATABASE_NAME;
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            return path;
        }
    }
}