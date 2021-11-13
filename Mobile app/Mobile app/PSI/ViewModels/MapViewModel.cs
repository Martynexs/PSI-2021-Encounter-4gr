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
        private string _destination;
        private double _routeduration;
        private double _routedistance;
        private MapService services;
        private DirectionResponse dr;
        private WaypointsCoordinatesService waypointsCoordinatesService;
        public static Map map;
        public Command GetRouteCommand { get; }
       
       


        public MapViewModel()
        {
            map = new Map();
            services = new MapService();
            dr = new DirectionResponse();
            waypointsCoordinatesService = new WaypointsCoordinatesService();
            GetRouteCommand = new Command(async () => await AddPolylineAsync());
           
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

        public async Task AddPolylineAsync()
        {
            Route route;
            List<Step> steps;
            List<Leg> legs;
            List<Intersection> intersections = new List<Intersection>();
            List<LatLong> locations = new List<LatLong>();
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

                    legs = route.Legs.ToList();
                    foreach (Leg leg in legs)
                    {
                        steps = leg.Steps.ToList();

                        foreach (Step step in steps)
                        {
                            List<Intersection> localIntersections = step.Intersections.ToList();

                            foreach (Intersection intersection in localIntersections)
                            {
                                intersections.Add(intersection);
                            }
                        }
                    }
                    foreach (Intersection intersection in intersections)
                    {
                        LatLong p = new LatLong
                        {
                            Lat = intersection.Location[1],
                            Long = intersection.Location[0]
                        };
                        locations.Add(p);
                    }

                    foreach (VisualWaypoint item in apiWaypoints)
                    {
                        Pin WaypointPins = new Pin()
                        {
                            Type = PinType.Place,
                            Label = item.Name,
                            Address = item.Description,
                            Position = new Position(item.Lat, item.Long),
                        };
                        map.Pins.Add(WaypointPins);
                    }


                    Polyline polyline = new Polyline
                    {
                        StrokeColor = Color.Blue,
                        StrokeWidth = 9,

                    };

                    foreach (LatLong latlong in locations)
                    {
                        polyline.Geopath.Add(new Position(latlong.Lat, latlong.Long));
                    }
                    map.MapElements.Add(polyline);

                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(apiWaypoints[0].Lat, apiWaypoints[0].Long),
                            Distance.FromKilometers(6));
                    map.MoveToRegion(mapSpan);
                }

                catch (Exception)
                {
                    await DisplayAlert("error: ", "Opps something went wong!", "ok");
                }
                finally
                {
                    IsBusy = false;
                }

            }
        }

 

    }
}