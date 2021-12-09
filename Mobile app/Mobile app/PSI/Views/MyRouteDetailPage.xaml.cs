using PSI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRouteDetailPage : ContentPage
    {
        public MyRouteDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MyRoutesAboutViewModel();
        }

        MyRoutesAboutViewModel _viewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}