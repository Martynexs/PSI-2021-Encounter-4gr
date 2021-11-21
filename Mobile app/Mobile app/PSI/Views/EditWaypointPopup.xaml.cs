using PSI.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWaypointPopup : PopupPage
    {
        public EditWaypointPopup()
        {
            InitializeComponent();
            EditWaypointViewModel vm = new EditWaypointViewModel();
            BindingContext = vm;
            EditWaypointViewModel.map = map;
            map.MapClicked += (_, e) => vm.LocationSelected(e);
        }
        async void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
	}
}