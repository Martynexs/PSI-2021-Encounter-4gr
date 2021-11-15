using DataLibrary.Models;
using PSI.ViewModels;
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
    public partial class NewWaypointPage : ContentPage
    {
        public Waypoint Waypoint { get; set; }

        public NewWaypointPage()
        {
            InitializeComponent();
            BindingContext = new NewWaypointViewModel();
        }
    }
}