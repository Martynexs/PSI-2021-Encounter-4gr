using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PSI.Models;
using PSI.Services;
using Map3.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using PSI.Views;
using Xamarin.Essentials;
using Map = Xamarin.Forms.Maps.Map;
using PSI.ViewModels;
using Map3.Views;
using System.Timers;
using System.Threading;

namespace Map3.ViewModels
{
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
            get { return _routeduration; }
            set { _routeduration = value; OnPropertyChanged(); }
        }
        public double RouteDistance
        {
            get { return _routedistance; }
            set { _routedistance = value; OnPropertyChanged(); }
        }
        public string ManeuverInfo
        {
            get { return _maneuverinfo; }
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
            if (!WalkingSession.HasGoalsLeft())
            {
                await DisplayAlert("Error:", "No waypoints to walk, please choose the route first.", "ok");
                return;
            }

            _walkingActive = true;
            Location deviceLocation = await Geolocation.GetLocationAsync();
           
            if (!WalkingSession.IsGoalReached(deviceLocation))
            {
                VisualWaypoint firstGoal = WalkingSession.CurrentGoal();
                await DisplayAlert("Info", "Hello, to start the route you need to reach first waypoint "  + firstGoal.Name + ". Please follow directions.", "Ok");
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
                        await HandleUserWalking(_walkingCancelHandler);
                        await task;
                    } else
                    {
                        WalkingSession.Finish();
                        _walkingActive = false;
                        return;
                    }
                } catch(TaskCanceledException)
                {
                    WalkingSession.Finish();
                    _walkingActive = false;
                }
            }
        }

        private async Task HandleUserWalking(CancellationTokenSource walkingCancelHandler)
        {
            Location deviceLocation = await Geolocation.GetLocationAsync();

            if (!WalkingSession.CheckMoved(deviceLocation))
            {
                return;
            }

            if (WalkingSession.IsGoalReached(deviceLocation))
            {
                if (WalkingSession.IsTheLastGoal())
                {
                    await DisplayAlert("Finish!", "You completed the route!", "ok");
                    WalkingSession.Finish();
                    walkingCancelHandler.Cancel();
                    map.MapElements.Clear();
                    map.Pins.Clear();
                    _walkingActive = false;
                    return;
                }
                else
                {
                    VisualWaypoint currentWaypoint = WalkingSession.CurrentGoal();
                    VisualWaypoint nextWaypoint = WalkingSession.MoveToNextGoal();
                    await DisplayAlert("Good job!", "You reached " + currentWaypoint.Name + " now please go to " + nextWaypoint.Name, "ok");
                    RedrawPolylineFromTo(currentWaypoint, nextWaypoint);
                    return;
                }
            }
            else
            {
                VisualWaypoint from = new VisualWaypoint();
                from.Lat = deviceLocation.Latitude;
                from.Long = deviceLocation.Longitude;
                VisualWaypoint to = WalkingSession.CurrentGoal();
                RedrawPolylineFromTo(from, to);
            }
            return;
        }

        private async void RedrawPolylineFromTo(VisualWaypoint from, VisualWaypoint to)
        {
            List<VisualWaypoint> fromTo = new List<VisualWaypoint>();
            fromTo.Add(from);
            fromTo.Add(to);

            DirectionResponse dr = await services.GetDirectionResponseAsync(fromTo);
            UpdateDistanceAndTime(dr);
            UpdateManeuver(dr);
            List<LatLong> polylineLocations = services.ExtractLocations(dr);
            map.MapElements.Clear();
            services.DrawPolyline(polylineLocations, map);

            MapSpan mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(fromTo), Distance.FromMeters(Math.Max(dr.Routes[0].Distance / 2, 50)));
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

                    List<VisualWaypoint> apiWaypoints = await waypointsCoordinatesService.LoadWaypointsFromAPI();

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

                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(apiWaypoints), Distance.FromKilometers(6));
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
            WalkingSession.Finish();
            _walkingCancelHandler.Cancel();
            map.MapElements.Clear();
            map.Pins.Clear();
            _walkingActive = false;
            await DisplayAlert("Info", "You quit this route without finishing it. Keep exploring other routes!", "ok");
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
            Route route = dr.Routes[0];
            RouteDuration = Math.Round((double)route.Duration / 60, 0);
            RouteDistance = Math.Round((double)route.Distance / 1000, 1);
        }

        private void UpdateManeuver(DirectionResponse dr)
        {
            Route route = dr.Routes[0];
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

            Step currentStep = route.Legs[0].Steps[0];
            Step nextStep = route.Legs[0].Steps[1];

            if (currentStep.Maneuver == null)
            {
                ManeuverInfo = "";
                return;
            }

            string maneuverType = (currentStep.Maneuver.Type == "depart") ? nextStep.Maneuver.Type : currentStep.Maneuver.Type;
            string maneuverModifier = (nextStep.Maneuver != null) ? nextStep.Maneuver.Modifier : "";
            string street = nextStep.Name;
            string toStreetString = (street != null && street != "") ? " to " + street : "";

            if (maneuverType != null && maneuverModifier != null)
            {
                ManeuverInfo = "In " + currentStep.Distance + " m. " + maneuverType + " " + maneuverModifier + toStreetString;
            } else
            {
                ManeuverInfo = "";
            }

            
        }

    }
}