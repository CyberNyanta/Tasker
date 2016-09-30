using System;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content;
using TinyIoC;
using Tasker.Droid.Utils;
using Tasker.Core.AL.Utils;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL;

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
        }


    }
}