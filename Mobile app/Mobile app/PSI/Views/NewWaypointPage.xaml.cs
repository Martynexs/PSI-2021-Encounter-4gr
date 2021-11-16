using DataLibrary.Models;
using PSI.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWaypointPage : ContentPage
    {
        public Waypoint Waypoint { get; set; }

        NewWaypointViewModel _viewModel;

        public NewWaypointPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewWaypointViewModel();
        }
    }
}