using PSI.ViewModels;
using Xamarin.Forms;

namespace PSI.Views
{
    public partial class RoutesPage : ContentPage
    {
        RoutesViewModel _viewModel;

        public RoutesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RoutesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}