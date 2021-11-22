using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSI.Models;
using PSI.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Map = Xamarin.Forms.Maps.Map;
using PSI.ViewModels;
using PSI.Views;
using System.Threading;

namespace Map3.ViewModels
{
    [QueryProperty(nameof(SelectedRouteId), nameof(SelectedRouteId))]
    public class MapViewModel : BaseViewModel
    {
        private bool _walkingActive;
        private double _routeduration;
        private double _routedistance;
        private string _maneuverinfo;
        private MapService services;
        private DirectionResponse dr;
        private WaypointsCoordinatesService waypointsCoordinatesService;
        private CancellationTokenSource _walkingCancelHandler;
        public static Map map;
        public Command GetRouteCommand { get; }
        public Command GoWalkingCommand { get; }
        public Command QuitWalkingCommand { get; }

        public long SelectedRouteId { get; set; }

        public MapViewModel()
        {
            map = new Map();
            services = new MapService();
            dr = new DirectionResponse();
            waypointsCoordinatesService = new WaypointsCoordinatesService();
            GetRouteCommand = new Command(async () => await AddPolylineAsync());
            GoWalkingCommand = new Command(async () => await InitializeWalkingProcess());
            QuitWalkingCommand = new Command(async () => await QuitWalkingProcess());
        }

        public double RouteDuration
        {
            get => _routeduration;
            set { _routeduration = value; OnPropertyChanged(); }
        }
        public double RouteDistance
        {
            get => _routedistance;
            set { _routedistance = value; OnPropertyChanged(); }
        }
        public string ManeuverInfo
        {
            get => _maneuverinfo;
            set { _maneuverinfo = value; OnPropertyChanged(); }
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        public async Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task InitializeWalkingProcess()
        {
            if (!WalkingSession.HasGoalWaypointsLeft())
            {
                await DisplayAlert("Error:", "No waypoints to walk, please choose the route first.", "ok");
                return;
            }

            _walkingActive = true;
            Location deviceLocation = await Geolocation.GetLocationAsync();

            if (!WalkingSession.IsGoalWaypointReached(deviceLocation))
            {
                VisualWaypoint firstGoal = WalkingSession.CurrentGoalWaypoint();
                await DisplayAlert("Info", "Hello, to start the route you need to reach first waypoint " + firstGoal.Name + ". Please follow directions.", "Ok");
            }

            _walkingCancelHandler = new CancellationTokenSource();
            HandleUserWalkingPeriodically(10);
        }

        private async void HandleUserWalkingPeriodically(int intervalSeconds)
        {
            while (true)
            {
                Task task = Task.Delay(TimeSpan.FromSeconds(intervalSeconds), _walkingCancelHandler.Token);
                try
                {
                    if (!_walkingCancelHandler.IsCancellationRequested)
                    {
                        await HandleUserWalking();
                        await task;
                    }
                    else
                    {
                        ClearWalkingState();
                        return;
                    }
                }
                catch (TaskCanceledException)
                {
                    ClearWalkingState();
                }
            }
        }

        private async Task HandleUserWalking()
        {
            var deviceLocation = await Geolocation.GetLocationAsync();

            if (!WalkingSession.CheckMoved(deviceLocation))
            {
                return;
            }

            if (WalkingSession.IsGoalWaypointReached(deviceLocation))
            {
                if (WalkingSession.IsTheLastGoalWaypoint())
                {
                    await DisplayAlert("Finish!", "You completed the route!", "ok");
                    ClearWalkingState();
                    return;
                }
                else
                {
                    var currentWaypoint = WalkingSession.CurrentGoalWaypoint();
                    var nextWaypoint = WalkingSession.MoveToNextGoalWaypoint();
                    await DisplayAlert("Good job!", "You reached " + currentWaypoint.Name + " now please go to " + nextWaypoint.Name, "ok");
                    RedrawPolylineFromTo(currentWaypoint, nextWaypoint);
                    return;
                }
            }
            else
            {
                var from = new VisualWaypoint();
                from.Lat = deviceLocation.Latitude;
                from.Long = deviceLocation.Longitude;
                VisualWaypoint to = WalkingSession.CurrentGoalWaypoint();
                RedrawPolylineFromTo(from, to);
            }
            return;
        }

        private async void RedrawPolylineFromTo(VisualWaypoint from, VisualWaypoint to)
        {
            var fromTo = new List<VisualWaypoint>();
            fromTo.Add(from);
            fromTo.Add(to);

            var directionResponse = await services.GetDirectionResponseAsync(fromTo);
            UpdateDistanceAndTime(directionResponse);
            UpdateManeuver(directionResponse);
            var polylineLocations = services.ExtractLocations(directionResponse);
            map.MapElements.Clear();
            services.DrawPolyline(polylineLocations, map);

            var mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(fromTo), Distance.FromMeters(Math.Max(directionResponse.Routes[0].Distance / 2, 50)));
            map.MoveToRegion(mapSpan);
        }

        public async Task AddPolylineAsync()
        {
            List<LatLong> locations;
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    RouteDuration = 0; RouteDistance = 0;

                    NetworkAccess current = Connectivity.NetworkAccess;

                    if (current != Connectivity.NetworkAccess)
                    {
                        await DisplayAlert("Error:", "You must be connected to the internet!", "ok");
                        return;
                    }

                    map.MapElements.Clear();
                    map.Pins.Clear();

                    List<VisualWaypoint> apiWaypoints = await waypointsCoordinatesService.LoadWaypointsFromAPI(SelectedRouteId);

                    dr = await services.GetDirectionResponseAsync(apiWaypoints);

                    if (dr == null)
                    {
                        await DisplayAlert("Error:", "Could not find route!", "ok");
                        return;
                    }

                    UpdateDistanceAndTime(dr);
                    ManeuverInfo = "";

                    locations = services.ExtractLocations(dr);

                    services.DrawPins(apiWaypoints, map);
                    services.DrawPolyline(locations, map);

                    var mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(apiWaypoints), Distance.FromKilometers(6));
                    map.MoveToRegion(mapSpan);
                    WalkingSession.ResetTo(apiWaypoints);
                }

                catch (Exception)
                {
                    await DisplayAlert("error: ", "Oops something went wrong!", "ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task QuitWalkingProcess()
        {
            if (!_walkingActive)
            {
                await DisplayAlert("Error", "You're not walking, there's nothing to stop.", "ok");
                return;
            }

            ClearWalkingState();

            var currentLocation = await Geolocation.GetLocationAsync();
            MapSpan.FromCenterAndRadius(new Position(currentLocation.Latitude, currentLocation.Longitude), Distance.FromKilometers(6));

            await DisplayAlert("Info", "You quit this route without finishing it. Keep exploring other routes!", "ok");
        }


        private void ClearWalkingState()
        {
            WalkingSession.Finish();
            _walkingCancelHandler.Cancel();
            if (_walkingCancelHandler != null)
            {
                _walkingCancelHandler.Cancel();
            }
            map.MapElements.Clear();
            map.Pins.Clear();
            _walkingActive = false;
            RouteDistance = 0;
            RouteDuration = 0;
            ManeuverInfo = "";
        }


            private Position GetVisualCenterPosition(List<VisualWaypoint> waypoints)
        {
            if (waypoints == null || waypoints.Count == 0)
            {
                return new Position(0, 0);
            }

            double LatSum = 0;
            double LongSum = 0;

            foreach (VisualWaypoint w in waypoints)
            {
                LatSum += w.Lat;
                LongSum += w.Long;
            }
            return new Position(LatSum / waypoints.Count, LongSum / waypoints.Count);
        }

        private void UpdateDistanceAndTime(DirectionResponse dr)
        {
            var route = dr.Routes[0];
            RouteDuration = Math.Round((double)route.Duration / 60, 0);
            RouteDistance = Math.Round((double)route.Distance / 1000, 1);
        }

        private void UpdateManeuver(DirectionResponse dr)
        {
            var route = dr.Routes[0];
            if (route == null || route.Legs == null || route.Legs.Count == 0 || route.Legs[0].Steps == null || route.Legs[0].Steps.Count < 2)
            {
                ManeuverInfo = "";
                return;
            }

            if (route.Distance < 50)
            {
                ManeuverInfo = "You're arriving!";
                return;
            }

            var departureStep = route.Legs[0].Steps[0];
            var turningStep = route.Legs[0].Steps[1];

            if (departureStep.Maneuver == null)
            {
                ManeuverInfo = "";
                return;
            }

            var maneuverType = turningStep.Maneuver.Type;
            var maneuverModifier = turningStep.Maneuver.Modifier;
            var street = turningStep.Name;
            var toStreetString = (street != null && street != "") ? " to " + street : "";

            if (maneuverType != null && maneuverModifier != null)
            {
                ManeuverInfo = "In " + departureStep.Distance + " m. " + maneuverType + " " + maneuverModifier + toStreetString;
            }
            else
            {
                ManeuverInfo = "";
            }
        }
    }
}