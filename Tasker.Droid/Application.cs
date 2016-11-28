using System;

using Android.App;
using Android.Runtime;

using TinyIoC;

using Tasker.Droid.AL.Utils;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL;
using Tasker.Core.BL.Contracts;
using Tasker.Core.BL.Managers;
using Tasker.Core.AL.ViewModels;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Core.DL;
using Tasker.Core.AL.Utils.Contracts;
using Android.Content.Res;
using System.Threading;
using System.Globalization;
using Java.Util;
using System.Collections.Generic;
using Tasker.Core.BL;

namespace Tasker.Droid
{
    [Application(Theme = "@style/Tasker")]
    public class Application : Android.App.Application
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public override void OnCreate()
        {
            base.OnCreate();
            var container = TinyIoCContainer.Current;

            container.Register<IDatabasePath, DatabasePath>();
            container.Register<IUnitOfWork, UnitOfWork>();
            container.Register<ITaskManager, TaskManager>();
            container.Register<IProjectManager, ProjectManager>();
            container.Register<IProjectListViewModel, ProjectListViewModel>();
            container.Register<ITaskListViewModel, TaskListViewModel>();
            container.Register<IProjectDetailsViewModel, ProjectDetailsViewModel>();
            container.Register<ITaskDetailsViewModel, TaskDetailsViewModel>();
            container.Register<INotificationUtils, NotificationUtils>();
            container.Register<IStatisticsProvider, StatisticsProvider>();
            container.Register<IStatisticsViewModel, StatisticsViewModel>();

            SetDotNetLocale(Application.Context.Resources.Configuration.Locale);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            SetDotNetLocale(newConfig.Locale);
            base.OnConfigurationChanged(newConfig);
        }

        private void SetDotNetLocale(Locale locale)
        {
            var code = locale.ToString().Substring(0, 2);
            if (Constans.Locales.Contains(code))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(code);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Constans.Locales[0]);
            }
        }

    }
}