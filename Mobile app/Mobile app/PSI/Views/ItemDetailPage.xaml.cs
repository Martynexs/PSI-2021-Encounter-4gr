using PSI.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PSI.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
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