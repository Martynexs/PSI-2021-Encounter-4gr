using PSI.ViewModels;
using Xamarin.Forms;

namespace PSI.Views
{
    public partial class MainPage : ContentPage
    {
        UsersStartedRoutesViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new UsersStartedRoutesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}