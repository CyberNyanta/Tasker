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
        }


    }
}