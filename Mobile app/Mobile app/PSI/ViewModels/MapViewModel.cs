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
using Map3.Views;
using System.Threading;
using DataLibrary;
using PSI.Views;
using DataLibrary.Models;
using System.Linq;

namespace Map3.ViewModels
{
    [QueryProperty(nameof(SelectedRouteId), nameof(SelectedRouteId))]
    public class MapViewModel : BaseViewModel
    {
        private readonly EncounterProcessor _encounterProcessor;
        private readonly MapService _mapService;
        private readonly WaypointsCoordinatesService waypointsCoordinatesService;

        private long _selectedRouteId;
        private double _routeduration;
        private double _routedistance;
        private string _maneuverinfo;
        private DirectionResponse dr;
        private CancellationTokenSource _walkingCancelHandler;
        public static Map map;
        public Command GoWalkingCommand { get; }
        public Command QuitWalkingCommand { get; }

        public MapViewModel()
        {
            map = new Map();
            _encounterProcessor = EncounterProcessor.Instanse;
            _mapService = new MapService();
            dr = new DirectionResponse();
            waypointsCoordinatesService = new WaypointsCoordinatesService();
            GoWalkingCommand = new Command(async () => await InitializeWalkingProcess());
            QuitWalkingCommand = new Command(async () => await QuitWalkingProcess());
        }

        public long SelectedRouteId
        {
            get => _selectedRouteId;
            set
            {
                _selectedRouteId = value;
                OnPropertyChanged();
                LoadRouteState(value);
            }
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

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        private async Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        private async Task SwitchToQuizWindow()
        {
            await Shell.Current.GoToAsync($"{nameof(QuizPopup)}");
        }

        private async void LoadRouteState(long selectedRoute)
        {
            if (!WalkingSession.IsWalkingStarted()
                || WalkingSession.GetCurrentRouteId() != selectedRoute
                || WalkingSession.GetCurrentGoalWaypoint() == null)
            {
                LoadFullRoute();
                WalkingSession.FinishAndClear();
                return;
            }

            if (WalkingSession.IsQuizStarted())
            {
                await DisplayAlert("Ooops", "You accidentally quit the quiz, don't worry we'll get you back.", "ok");
                await SwitchToQuizWindow();
                return;
            }

            // Walking already in this route? Refresh it:
            map.Pins.Clear();
            _mapService.DrawPins(WalkingSession.GetAllWaypoints(), map);
            HandleUserWalkingPeriodically(10);
        }

        public async Task InitializeWalkingProcess()
        {
            if (WalkingSession.IsWalkingStarted())
            {
                await DisplayAlert("Error", "Sorry, you're already walking", "ok");
                return;
            }
            
            if (!WalkingSession.HasGoalWaypointsLeft())
            {
                await DisplayAlert("Error", "No waypoints to walk, please choose the route first.", "ok");
                return;
            }

            WalkingSession.Start();
            
            Location deviceLocation = await Geolocation.GetLocationAsync();

            VisualWaypoint pickedWaypoint = null;
            while (pickedWaypoint == null)
            {
                pickedWaypoint = await PickNextWaypoint(deviceLocation);
            }
            WalkingSession.SetPickedWaypoint(pickedWaypoint);

            if (!WalkingSession.IsGoalWaypointReached(deviceLocation))
            {
                VisualWaypoint firstGoal = WalkingSession.GetCurrentGoalWaypoint();
                await DisplayAlert("Info", "Hello, to start the route you need to reach first waypoint " + firstGoal.Name + ". Please follow directions.", "Ok");
            }

            HandleUserWalkingPeriodically(10);
        }

        private async void HandleUserWalkingPeriodically(int intervalSeconds)
        {
            _walkingCancelHandler = new CancellationTokenSource();
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
                        ClearWalkingState(false);
                        return;
                    }
                }
                catch (TaskCanceledException)
                {
                    ClearWalkingState(false);
                }
            }
        }

        private async Task HandleUserWalking()
        {
            Location deviceLocation = await Geolocation.GetLocationAsync();

            if (WalkingSession.HasQuiz())
            {
                _walkingCancelHandler.Cancel(); // no walking, quiz is active
                return;
            }

            if (!WalkingSession.CheckMoved(deviceLocation) && !WalkingSession.HasQuizJustFinished())
            {
                return; // do nothing since user doesn't move
            }

            if (WalkingSession.IsGoalWaypointReached(deviceLocation))
            {
                var currentWaypoint = WalkingSession.GetCurrentGoalWaypoint();

                // Expect quiz:
                List<Quiz> questions;
                if (!WalkingSession.HasQuizJustFinished())
                {
                    questions = await _encounterProcessor.GetMultipleWaypointQuestions(currentWaypoint.Id);
                }
                else
                {
                    questions = null;
                    WalkingSession.ClearQuizJustFinishedMarker();
                }
                
                
                if (questions != null && questions.Count > 0)
                {
                    WalkingSession.StartQuiz(questions);
                    await DisplayAlert("Quiz found!", "You found " + currentWaypoint.Name + " and here's a task for you! Check your knowledge before proceeding!", "ok");
                    await SwitchToQuizWindow();
                    return;
                }
                
                // No quiz, deal as usual:
                if (WalkingSession.IsTheLastGoalWaypoint())
                {
                    await DisplayAlert("Finish!", "You completed the route!", "ok");
                    ClearWalkingState(true);
                    LoadFullRoute();
                    return;
                }
                else
                {
                    WalkingSession.MarkCurrentGoalWaypointReached();

                    VisualWaypoint pickedWaypoint = null;

                    // One waypoint left, choose for user, since it's no point to ask choosing from one option:
                    if (WalkingSession.GetLeftWaypoints().Count == 1)
                    {
                        pickedWaypoint = WalkingSession.GetLeftWaypoints()[0];
                    }

                    while (pickedWaypoint == null)
                    {
                        pickedWaypoint = await PickNextWaypoint(deviceLocation);
                    }
                    var nextWaypoint = WalkingSession.MoveToNextWaypoint(pickedWaypoint);
                    await DisplayAlert("Good job!", "Now please go to " + nextWaypoint.Name, "ok");
                    RedrawPolylineFromTo(currentWaypoint, nextWaypoint);
                    return;
                }
            }
            else
            {
                var from = new VisualWaypoint();
                from.Lat = deviceLocation.Latitude;
                from.Long = deviceLocation.Longitude;
                VisualWaypoint to = WalkingSession.GetCurrentGoalWaypoint();
                RedrawPolylineFromTo(from, to);
            }
            return;
        }

        private async Task<VisualWaypoint> PickNextWaypoint(Location deviceLocation)
        {
            List<string> pickedStrings = new List<string>();
            foreach (VisualWaypoint wp in WalkingSession.GetLeftWaypoints())
            {
                double airDistance = Location.CalculateDistance(deviceLocation.Latitude, deviceLocation.Longitude, wp.Lat, wp.Long, DistanceUnits.Kilometers);
                string visualString = wp.Name + " (" + Math.Round(airDistance, 2) + "km)";
                wp.PickString = visualString;
                pickedStrings.Add(visualString);
            }
            string[] pickedStringsArray = pickedStrings.ToArray();
            string resultString = await DisplayActionSheet(title: "Pick a waypoint (straight line distance)", cancel: "", destruction: "", buttons: pickedStringsArray);

            VisualWaypoint resultWaypoint = WalkingSession.GetLeftWaypoints().FirstOrDefault(w => w.PickString.Equals(resultString));
            return resultWaypoint;
        }

        private async void RedrawPolylineFromTo(VisualWaypoint from, VisualWaypoint to)
        {
            var fromTo = new List<VisualWaypoint>();
            fromTo.Add(from);
            fromTo.Add(to);

            var directionResponse = await _mapService.GetDirectionResponseAsync(fromTo);
            UpdateDistanceAndTime(directionResponse);
            UpdateManeuver(directionResponse);
            var polylineLocations = _mapService.ExtractLocations(directionResponse);
            map.MapElements.Clear();
            _mapService.DrawPolyline(polylineLocations, map);

            var mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(fromTo), Distance.FromMeters(Math.Max(directionResponse.Routes[0].Distance / 2, 50)));
            map.MoveToRegion(mapSpan);
        }

        public async void LoadFullRoute()
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

                    dr = await _mapService.GetDirectionResponseAsync(apiWaypoints);

                    if (dr == null)
                    {
                        await DisplayAlert("Error:", "Could not find route!", "ok");
                        return;
                    }

                    UpdateDistanceAndTime(dr);
                    ManeuverInfo = "";

                    locations = _mapService.ExtractLocations(dr);

                    _mapService.DrawPins(apiWaypoints, map);
                    _mapService.DrawPolyline(locations, map);

                    var mapSpan = MapSpan.FromCenterAndRadius(GetVisualCenterPosition(apiWaypoints), Distance.FromKilometers(6));
                    map.MoveToRegion(mapSpan);
                    WalkingSession.InitializeWaypointsBeforeWalking(apiWaypoints, SelectedRouteId);
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
            if (!WalkingSession.IsWalkingStarted())
            {
                await DisplayAlert("Error", "You're not walking, there's nothing to stop.", "ok");
                return;
            }

            ClearWalkingState(true);

            LoadFullRoute();

            await DisplayAlert("Info", "You quit this route without finishing it. Keep exploring other routes!", "ok");
        }


        private void ClearWalkingState(bool clearWalkingSession)
        {
            if (clearWalkingSession)
            {
                WalkingSession.FinishAndClear();
            }
           
            if (_walkingCancelHandler != null)
            {
                _walkingCancelHandler.Cancel();
            }
            map.MapElements.Clear();
            map.Pins.Clear();
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