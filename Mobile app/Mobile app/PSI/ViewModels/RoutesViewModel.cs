using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class RoutesViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        private Session _session;
        public ObservableCollection<Route> Routes { get; }

        private List<Waypoint> _waypointList = new List<Waypoint>();
        public Command LoadRoutesCommand { get; }
        public Command LoadUserRoutesCommand { get; }
        public Command AddRouteCommand { get; }

        private Route _selectedRoute;

        private bool _userRoutesOnly = false;
        public string SearchText { get; set; }

        public RoutesViewModel()
        {
            Title = "Routes";

            Routes = new ObservableCollection<Route>();

            LoadRoutesCommand = new Command(async () => await ExecuteLoadRoutesCommand());

            LoadUserRoutesCommand = new Command(LoadUserRoutes);

            AddRouteCommand = new Command(AddRoute);

            _encounterProcessor = EncounterProcessor.Instanse;
            _encounterProcessor.UnauthorisedHttpRequestEvent += OnAuthenticationFailed;

            _session = Session.Instanse;
        }

        IOrderedEnumerable<Waypoint> objectsQueryOrderedByDistance;
        public async void Distance(Route route)
        {
            _waypointList = await _encounterProcessor.GetWaypoints(route.Id);
            var request = new GeolocationRequest(GeolocationAccuracy.Default);
            var location = await Geolocation.GetLocationAsync(request);

            foreach (var pin in _waypointList)
            {

                    pin.DistanceToUser = Location.CalculateDistance(location.Latitude, location.Longitude, pin.Latitude, pin.Longitude, DistanceUnits.Kilometers);
            }
            if( _waypointList.Count != 0)
            {
                objectsQueryOrderedByDistance = _waypointList.OrderBy(pin => pin.DistanceToUser);
                route.Distances = Math.Round(objectsQueryOrderedByDistance.FirstOrDefault().DistanceToUser,2);
                Routes.Add(route);
            }
            else
            {
                Routes.Add(route);
            }
        }
        async Task ExecuteLoadRoutesCommand()
        {
            if (!_userRoutesOnly)
            {
                IsBusy = true;

                try
                {
                    Routes.Clear();
                    var routes = await _encounterProcessor.GetAllRoutes();

                    if (SearchText != "" && SearchText != null) routes = SearchRoutes(routes);

                    foreach (var route in routes)
                    {
                        Distance(route);
                        //Routes.Add(route);
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
            IsBusy = false;
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                _selectedRoute = value;
                OnRouteSelected(value);
            }
        }
        private async void OnRouteSelected(Route route)
        {
            await Shell.Current.GoToAsync($"{nameof(RouteDetailPage)}?{nameof(WaypointsViewModel.RoutesId)}={route.Id}");
        }

        private async void AddRoute(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRoutePage));
        }
        private async void OnAuthenticationFailed()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void LoadUserRoutes()
        {
            _userRoutesOnly = !_userRoutesOnly;

            if (_userRoutesOnly)
            {
                IsBusy = true;
                try
                {
                    Routes.Clear();
                    var items = await _encounterProcessor.GetUserRoutes(_session.CurrentUser.Id);
                    foreach (var item in items)
                    {
                        Routes.Add(item);
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
        }


        private List<Route> SearchRoutes(List<Route> routes)
        {
            var smthg = routes;
            var searchedRoutes = routes.Where(r => r.Name.Contains(SearchText)).Select(r => r);
            return searchedRoutes.ToList();
        }

    }
}