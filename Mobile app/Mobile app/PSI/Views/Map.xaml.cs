using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Collections.Generic;
using PSI.ViewModels;
using System;

using Xamarin.Forms.Xaml;
using Map3.ViewModels;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        public Map()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();
            MapViewModel.map = map;
        }
    }
}