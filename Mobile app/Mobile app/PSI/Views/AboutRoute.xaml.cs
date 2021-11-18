using PSI.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutRoute : ContentPage
    {
        public AboutRoute()
        {
            InitializeComponent();
            BindingContext = new RouteDetailViewModel();
        }
    }
}