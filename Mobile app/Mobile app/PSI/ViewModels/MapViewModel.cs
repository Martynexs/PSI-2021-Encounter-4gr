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
       
        private string _origin;
        private string _destination;
        private double _routeduration;
        private double _routedistance;
        private MapService services;
        private DirectionResponse dr;
        private WaypointsCoordinatesService waypointsCoordinatesService;
        public static Map map;
        public Command GetRouteCommand { get; }
        public Command GoWalkingCommand { get; }




        public MapViewModel()
        {
            map = new Map();
            services = new MapService();
            dr = new DirectionResponse();
            waypointsCoordinatesService = new WaypointsCoordinatesService();
            GetRouteCommand = new Command(async () => await AddPolylineAsync());
            GoWalkingCommand = new Command(async () => await InitializeWalkingProcess());
        }

        public string Origin
        {
            get { return _origin; }
            set { _origin = value; OnPropertyChanged(); }
        }
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; OnPropertyChanged(); }
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

            Location deviceLocation = await Geolocation.GetLocationAsync();
           
            if (!WalkingSession.IsGoalReached(deviceLocation))
            {
                VisualWaypoint firstGoal = WalkingSession.CurrentGoal();
                await DisplayAlert("Info", "Hello, to start the route you need to reach first waypoint "  + firstGoal.Name + ". Please follow directions.", "Ok");
            }

            CancellationTokenSource cancellation = new CancellationTokenSource();
            HandleUserWalkingPeriodically(TimeSpan.FromSeconds(10), cancellation.Token);
            //Timer updateUserLocationTimer = new Timer();
            //updateUserLocationTimer.Interval = 15 * 1000;
            //updateUserLocationTimer.Elapsed += new ElapsedEventHandler(HandleUserWalking);
            //updateUserLocationTimer.Enabled = true;
        }

        private async void HandleUserWalkingPeriodically(TimeSpan interval, CancellationToken cancellationToken)
        {
            //_ = HandleUserWalking(sender, eventArgs);
            //_ = Task.Run(async () => await HandleUserWalking(sender, eventArgs));
            while (true)
            {
                Task task = Task.Delay(interval, cancellationToken);
                try
                {
                    await HandleUserWalking();
                    await task;
                } catch(TaskCanceledException)
                {

                }
            }
        }

        private async Task HandleUserWalking()
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
                    map.MapElements.Clear();
                    return;
                }
                else
                {
                    VisualWaypoint currentWaypoint = WalkingSession.CurrentGoal();
                    VisualWaypoint nextWaypoint = WalkingSession.MoveToNextGoal();
                    await DisplayAlert("Good job!", "You reached " + currentWaypoint.Name + " now please go to " + nextWaypoint.Name, "ok");

                    map.MapElements.Clear();
                    List<VisualWaypoint> fromTo = new List<VisualWaypoint>();
                    fromTo.Add(currentWaypoint);
                    fromTo.Add(nextWaypoint);
                    DirectionResponse dr = await services.GetDirectionResponseAsync(fromTo);
                    List<LatLong> polylineLocations = services.ExtractLocations(dr);
                    services.DrawPolyline(polylineLocations, map);
                    return;
                }
            }
            else
            {
                map.MapElements.Clear();

                List<VisualWaypoint> fromTo = new List<VisualWaypoint>();
                VisualWaypoint from = new VisualWaypoint();
                from.Lat = deviceLocation.Latitude;
                from.Long = deviceLocation.Longitude;

                VisualWaypoint to = WalkingSession.CurrentGoal();

                fromTo.Add(from);
                fromTo.Add(to);
                DirectionResponse dr = await services.GetDirectionResponseAsync(fromTo);
                List<LatLong> polylineLocations = services.ExtractLocations(dr);
                services.DrawPolyline(polylineLocations, map);
            }
            return;
        }

        public async Task AddPolylineAsync()
        {
            Route route;
            
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
                    
                        
                    route = dr.Routes[0];

                    RouteDuration = Math.Round((double)route.Duration / 60, 0);
                    RouteDistance = Math.Round((double)route.Distance / 1000, 1);

                    locations = services.ExtractLocations(dr);

                    services.DrawPins(apiWaypoints, map);


                    services.DrawPolyline(locations, map);

                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(apiWaypoints),
                            Distance.FromKilometers(6));
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

    }
}