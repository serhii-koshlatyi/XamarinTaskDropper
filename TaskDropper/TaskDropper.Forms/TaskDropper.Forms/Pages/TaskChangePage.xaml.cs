﻿using MvvmCross.Forms.Views;
using System;
using System.Diagnostics;
using TaskDropper.Core.ViewModels;

using Xamarin.Forms.Xaml;


namespace TaskDropper.Forms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskChangePage : MvxContentPage<TaskChangedViewModel>
    {
      
        public TaskChangePage()
        {
            InitializeComponent();
        }

        private async void AttachPhotoClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Attach Photo", "Cancel", null, "From gallery", "From Camera");
            Debug.WriteLine("Action: " + action);

            if (action == "From gallery")
            {
                ViewModel.ChoosePictureCommand.Execute();
            }
            if (action == "From Camera")
            {
                if (!ViewModel.CheckPermissionForCamera())
                {
                    ViewModel.AddPermission();
                }
                ViewModel.TakePictureCommand.Execute();
            }
        }
    }
}