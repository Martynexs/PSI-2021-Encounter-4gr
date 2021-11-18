using PSI.ViewModels;
using Xamarin.Forms;

namespace PSI.Views
{
    public partial class RouteDetailPage : ContentPage
    {
        public RouteDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new WaypointsViewModel();
        }

        WaypointsViewModel _viewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}