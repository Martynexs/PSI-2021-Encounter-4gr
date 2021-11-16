using DataLibrary;
using DataLibrary.Models;
using Map3.ViewModels;
using PSI.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RoutesId),nameof(RoutesId))]
    class WaypointsViewModel : BaseViewModel
    {
        public Command LoadWaypointsCommand { get; }

        private Waypoint _selectedWaypoint;
        public Command WaypointInfoCommand { get; }
        public Command WaypointEditCommand { get; }
        public Command WaypointDeleteCommand { get; }
        public Command RouteInfoCommand { get; }
        public Command RouteDeleteCommand { get; }
        public Command AddWaypointCommand { get; }
        public Command OpenMapCommand { get; }
        public ObservableCollection<Waypoint> Waypoints { get; }
        public Command<Waypoint> WaypointTapped { get; }

        private EncounterProcessor _encounterProcessor;

        public long routeId;
        public WaypointsViewModel()
        {
            Waypoints = new ObservableCollection<Waypoint>();

            OpenMapCommand = new Command(OpenMapView);

            WaypointTapped = new Command<Waypoint>(OnWaypointSelected);

            WaypointInfoCommand = new Command(OnWaypointClicked);

            WaypointEditCommand = new Command(OnWaypointEditClicked);

            LoadWaypointsCommand = new Command(async () => await ExecuteLoadIWaypointsCommand());

            WaypointDeleteCommand = new Command(OnWaypointDeleteClicked);

            RouteInfoCommand = new Command(OnAboutRouteClicked);

            RouteDeleteCommand = new Command(OnRouteDeleteClicked);

            AddWaypointCommand = new Command(OnAddWaypoint);

            _encounterProcessor = EncounterProcessor.Instanse;
        }

        public long RoutesId
        {
            get => routeId;
            set => routeId = value;
        }
        async Task ExecuteLoadIWaypointsCommand()
        {
            IsBusy = true;
            try
            {
                Waypoints.Clear();
                var items = await _encounterProcessor.GetWaypoints(routeId);
                foreach (var item in items)
                {
                    Waypoints.Add(item);
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

        private async void OnWaypointDeleteClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}");
            await _encounterProcessor.DeleteWaypoint(SelectedWaypoint.Id);
        }
        private async void OnWaypointClicked(object obj)
        {
            await Shell.Current.GoToAsync(nameof(WaypointInfo));
        }

        private async void OnWaypointEditClicked(object sender)
        {
            //await PopupNavigation.Instance.PushAsync(new EditWaypointPopup());
        }
        private async void OnAboutRouteClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(AboutRoute)}?{nameof(ItemDetailViewModel.RouteId)}={routeId}");
        }

        private async void OnRouteDeleteClicked(object obj)
        {
            await Shell.Current.GoToAsync("..");
            await _encounterProcessor.DeleteRoute(routeId);
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async void OnAddWaypoint(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewWaypointPage)}?{nameof(NewWaypointViewModel.RoutesId)}={routeId}");
        }
        public Waypoint SelectedWaypoint
        {
            get => _selectedWaypoint;
            set
            {
                _selectedWaypoint = value;
                OnWaypointSelected(value);
            }
        }
        private async void OnWaypointSelected(Waypoint waypoint)
        {
            await Shell.Current.GoToAsync($"{nameof(WaypointInfo)}?{nameof(WaypointViewModel.WaypointId)}={waypoint.Id}");
        }

        private async void OpenMapView()
        {
            await Shell.Current.GoToAsync($"{nameof(Map)}?{nameof(MapViewModel.SelectedRouteId)}={routeId}");
        }
    }
}
