using PSI.Views;
using System;
using Xamarin.Forms;

namespace PSI
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RouteDetailPage), typeof(RouteDetailPage));
            Routing.RegisterRoute(nameof(NewRoutePage), typeof(NewRoutePage));
            Routing.RegisterRoute(nameof(WaypointInfo), typeof(WaypointInfo));
            Routing.RegisterRoute(nameof(AboutRoute), typeof(AboutRoute));
            Routing.RegisterRoute(nameof(NewWaypointPage), typeof(NewWaypointPage));
            Routing.RegisterRoute(nameof(RouteEditPopup), typeof(RouteEditPopup));
            Routing.RegisterRoute(nameof(EditWaypointPopup), typeof(EditWaypointPopup));
            Routing.RegisterRoute(nameof(Map), typeof(Map));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
