using PSI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRouteWaypointPage : ContentPage
    {
        public MyRouteWaypointPage()
        {
            InitializeComponent();
            BindingContext = new WaypointDetailViewModel();
            WaypointDetailViewModel.map = map;
        }
    }
}