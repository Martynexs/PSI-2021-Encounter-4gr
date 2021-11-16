using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWaypointPopup : PopupPage
    {
        public EditWaypointPopup()
        {
            InitializeComponent();
        }

        async void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}