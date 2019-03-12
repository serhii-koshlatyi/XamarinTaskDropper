﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using TaskDropper.Core.Interface;
using TaskDropper.Forms.iOS.PageRenderers;
using TaskDropper.Forms.iOS.Services;
using UIKit;

namespace TaskDropper.Forms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, App>
    {
    
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //GoogleLoginRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            var uri_netfx = new Uri(url.AbsoluteString);

            GoogleLoginRenderer.Auth?.OnPageLoading(uri_netfx);

            return true;
        }
    }
}