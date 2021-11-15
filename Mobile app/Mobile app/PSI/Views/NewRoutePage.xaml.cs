using DataLibrary.Models;
using PSI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    public partial class NewRoutePage : ContentPage
    {
        public Route Item { get; set; }

        public NewRoutePage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}