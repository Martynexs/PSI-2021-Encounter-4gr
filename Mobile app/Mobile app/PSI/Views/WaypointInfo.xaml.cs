using PSI.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaypointInfo : ContentPage
    {
        public WaypointInfo()
        {
            InitializeComponent();
            BindingContext = new WaypointsViewModel();
        }
    }
}