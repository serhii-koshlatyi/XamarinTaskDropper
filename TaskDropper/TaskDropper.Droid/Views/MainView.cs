﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using TaskDropper.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Views.InputMethods;
using Android.Widget;

namespace TaskDropper.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "ViewPager", MainLauncher = true)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {

        public DrawerLayout DrawerLayout { get; set; }
        private InputMethodManager _imm;
        private LinearLayout _linearLayoutMain;

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);


            SetContentView(Resource.Layout.main_view);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
         

            if (bundle == null)
            {
                ViewModel.ShowTaskListViewModelCommand.Execute(null);
            }
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
            {
                OnBackPressed();
            }

            return base.OnOptionsItemSelected(item);
        }


    }
    }