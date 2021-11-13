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

namespace Map3.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private string _origin;

        public string Origin
        {
            get { return _origin; }
            set { _origin = value; OnPropertyChanged(); }
        }
        private string _destination;

        public string Destination
        {
            get { return _destination; }
            set { _destination = value; OnPropertyChanged(); }
        }

        private double _routeduration;

        public double RouteDuration
        {
            get { return _routeduration; }
            set { _routeduration = value; OnPropertyChanged(); }
        }

        private double _routedistance;

        public double RouteDistance
        {
            get { return _routedistance; }
            set { _routedistance = value; OnPropertyChanged(); }
        }

        public static Map map;
        public Command GetRouteCommand { get; }
        public Command GetAllWaypointsCommand { get; }
        private MapService services;
        private DirectionResponse dr;
        private LatLong latLong;
        private WaypointsCoordinatesService waypointsCoordinatesService;
        
        public MapViewModel()
        {
            map = new Map();
            services = new MapService();
            dr = new DirectionResponse();
            latLong = new LatLong();
            waypointsCoordinatesService = new WaypointsCoordinatesService();
         
            GetRouteCommand = new Command(async () => await addPolylineAsync(Origin, Destination));
            GetAllWaypointsCommand = new Command(() => LoadWaypointButton(latLong.Lat, latLong.Long));
           

        }
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        public async Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        

        public async Task addPolylineAsync(string origin, string destination)
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    RouteDuration = 0; RouteDistance = 0;

                    var current = Xamarin.Essentials.Connectivity.NetworkAccess;

                    if (current != Xamarin.Essentials.Connectivity.NetworkAccess)
                    {
                        await DisplayAlert("Error:", "You must be connected to the internet!", "ok");
                        return;
                    }
                    if (origin == null || destination == null)
                    {
                        await DisplayAlert("Error:", "Origin and destination can not be empty!", "ok");
                        return;
                    }

                    map.MapElements.Clear();
                    map.Pins.Clear();
                    List<Route> routes = new List<Route>();
                    List<Leg> legs = new List<Leg>();
                    List<Step> steps = new List<Step>();
                    List<Intersection> intersections = new List<Intersection>();
                    List<LatLong> locations = new List<LatLong>();
                    Maneuver maneuver = new Maneuver();

                    dr = await services.GetDirectionResponseAsync(origin, destination);

                    if (dr == null)
                    {
                        await DisplayAlert("Error:", "Could not find route!", "ok");
                        return;
                    }
                    if (dr != null)
                    {
                        routes = dr.Routes.ToList();

                        RouteDuration = Math.Round((Double)routes[0].Duration / 60, 0);
                        RouteDistance = Math.Round((Double)routes[0].Distance/1000, 1);
                    }
                    foreach (var route in routes)
                    {
                        legs = route.Legs.ToList();
                    }
                    foreach (var leg in legs)
                    {
                        steps = leg.Steps.ToList();
                    }
                    foreach (var step in steps)
                    {
                        var localIntersections = step.Intersections.ToList();

                        foreach (var intersection in localIntersections)
                        {
                            intersections.Add(intersection);

                        }
                    }
                    foreach (var intersection in intersections)
                    {
                        LatLong p = new LatLong();
                        p.Lat = intersection.Location[1];
                        p.Long = intersection.Location[0];
                        locations.Add(p);


                    }
                    foreach (var step in steps)
                    {
                        maneuver = step.Maneuver;
                    }

                    Polyline polyline = new Polyline
                    {
                        StrokeColor = Color.Blue,
                        StrokeWidth = 9,

                    };

                    foreach (var latlong in locations)
                    {
                        polyline.Geopath.Add(new Position(latlong.Lat, latlong.Long));

                    }
                    map.MapElements.Add(polyline);

                    var firstPinLocation = locations[0];
                    var lastPinLocation = locations[locations.Count() - 1];

                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(firstPinLocation.Lat, firstPinLocation.Long),
                        Distance.FromKilometers(6));
                    map.MoveToRegion(mapSpan);



                    Pin firstpoint = new Pin()
                    {
                        Label = "Origin",
                        Address = Origin,
                        Type = PinType.SearchResult,
                        Position = new Position(firstPinLocation.Lat, firstPinLocation.Long),
                    };
                    map.Pins.Add(firstpoint);

                    Pin lastpoint = new Pin()
                    {
                        Label = "Destination",
                        Address = Destination,
                        Type = PinType.SearchResult,
                        Position = new Position(lastPinLocation.Lat, lastPinLocation.Long),
                    };
                    map.Pins.Add(lastpoint);

                }

                catch (Exception e)
                {
                    await DisplayAlert("error: ", "Opps something went wong!", "ok");
                }
                finally
                {
                    IsBusy = false;
                }

            }
        }

        public async void LoadWaypointButton(double Lat, double Long)
        {
            var contents = await waypointsCoordinatesService.LoadWaypoints();

            if(contents != null)
            {
                foreach(var item in contents)
                {
                    Pin WaypointPins = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "waypoint",
                        Address = " i don know",
                        Position = new Position(item.Lat, item.Long),
                    };
                    map.Pins.Add(WaypointPins);

                }
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(contents[0].Lat, contents[0].Long),
                Distance.FromKilometers(100));
                map.MoveToRegion(mapSpan);



            }

        }

       


    }
}