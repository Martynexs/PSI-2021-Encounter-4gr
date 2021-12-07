using DataLibrary;
using DataLibrary.Models;
using Map3.ViewModels;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RoutesId), nameof(RoutesId))]
    public class WaypointsViewModel : BaseViewModel
    {
        public Command LoadWaypointsCommand { get; }

        private Waypoint _selectedWaypoint;

        private Session _session;
        public Command RouteInfoCommand { get; }
        public Command RouteDeleteCommand { get; }
        public Command AddWaypointCommand { get; }
        public Command OpenMapCommand { get; }
        public ObservableCollection<Waypoint> Waypoints { get; }

        public List<WaypointCompletion> CompletedWaypoints = new List<WaypointCompletion>();

        private EncounterProcessor _encounterProcessor;

        public long routeId;
        public WaypointsViewModel()
        {
            Title = "Waypoints";

            Waypoints = new ObservableCollection<Waypoint>();

            OpenMapCommand = new Command(OpenMapView);

            LoadWaypointsCommand = new Command(async () => await ExecuteLoadIWaypointsCommand());

            RouteInfoCommand = new Command(OnAboutRouteClicked);

            RouteDeleteCommand = new Command(OnRouteDeleteClicked);

            AddWaypointCommand = new Command(OnAddWaypoint);

            _encounterProcessor = EncounterProcessor.Instanse;

            _session = Session.Instanse;
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
                if (CompletedWaypoints != null)
                {
                    CompletedWaypoints.Clear();
                }
                var completedWaypoints = await _encounterProcessor.GetWaypointCompletions(routeId, _session.CurrentUser.Id);
                foreach (var cwaypoints in completedWaypoints)
                {
                    CompletedWaypoints.Add(cwaypoints);
                }

                Waypoints.Clear();
                var waypoints = await _encounterProcessor.GetWaypoints(routeId);
                foreach (var waypoint in waypoints)
                {
                    if (CompletedWaypoints != null)
                    {
                        foreach (var cwaypoints in CompletedWaypoints)
                        {
                            if (cwaypoints.WaypointId == waypoint.Id)
                            {
                                waypoint.Color = "Completed";
                            }
                        }
                    }
                    Waypoints.Add(waypoint);
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
        private async void OnAboutRouteClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(AboutRoute)}?{nameof(RouteDetailViewModel.RouteId)}={routeId}");
        }

        private async void OnRouteDeleteClicked(object obj)
        {
            await _encounterProcessor.DeleteRoute(routeId);
            await Shell.Current.GoToAsync("..");
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
            await Shell.Current.GoToAsync($"{nameof(WaypointInfo)}?{nameof(WaypointDetailViewModel.WaypointId)}={waypoint.Id}");
        }

        private async void OpenMapView()
        {
            await Shell.Current.GoToAsync($"{nameof(Map)}?{nameof(MapViewModel.SelectedRouteId)}={routeId}");
        }
    }
}
