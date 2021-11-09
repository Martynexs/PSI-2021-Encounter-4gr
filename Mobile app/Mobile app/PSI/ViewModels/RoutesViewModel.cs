using DataLibrary;
using DataLibrary.Models;
using PSI.Models;
using PSI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Route> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command WaypointInfoCommand { get; }
        public Command WaypointEditCommand { get; }

        public Command RouteEditCommand { get; }
        public Command RouteInfoCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> WaypointTapped { get; }

        public ItemsViewModel()
        {
            Title = "Routes";
            Items = new ObservableCollection<Route>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            WaypointTapped = new Command<Item>(OnWaypointSelected);

            WaypointInfoCommand = new Command(OnWaypointClicked);

            WaypointEditCommand = new Command(OnWaypointEditClicked);

            RouteEditCommand = new Command(OnRouteEditClicked);

            RouteInfoCommand = new Command(OnAboutRouteClicked);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await EncounterProcessor.GetAllRoutes();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRoutePage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.RoutesId)}={item.Id}");
        }

        async void OnWaypointSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync(nameof(WaypointInfo));
        }

        private async void OnWaypointClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync(nameof(WaypointInfo));
        }

        private async void OnWaypointEditClicked(object sender)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await PopupNavigation.Instance.PushAsync(new EditWaypointPopup());
        }

        private async void OnRouteEditClicked(object sender)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await PopupNavigation.Instance.PushAsync(new RouteEditPopup());
        }

        private async void OnAboutRouteClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync(nameof(AboutRoute));
        }
    }
}