﻿using PSI.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadProfilePhotoPage :  PopupPage
    {
        public UploadProfilePhotoPage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
    }
}