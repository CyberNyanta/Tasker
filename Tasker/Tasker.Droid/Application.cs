using System;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content;

namespace Tasker.Droid
{
    [Application(Theme = "@style/Tasker")]
    public class Application : Android.App.Application
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public override void OnCreate()
        {

            base.OnCreate();

        }


    }
}