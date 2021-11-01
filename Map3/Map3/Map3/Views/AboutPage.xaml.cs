using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Collections.Generic;
using Map3.ViewModels;
using System;

namespace Map3.Views
{
    public partial class AboutPage : ContentPage
    {
        /*   private readonly Geocoder _geocoder = new Geocoder();
        CustomMap customMap = new CustomMap();
        aboutViewModel aboutViewModel;
        private string firstLatitude;
        private string firstLongitude;
        */

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new RouteViewModel();
            RouteViewModel.map = map;

          //  DrawPolygon();
            

           // DrawMap();

        }

        /*  private void DrawMap()
             {
                 Position onePosition = new Position(54.684914,25.293809);
                 Position twoPosition = new Position(54.6858,25.2877);
                 List<Position> listPosition = new List<Position>();
                 listPosition.Add(onePosition);
                 listPosition.Add(twoPosition);

             var firstWaypoint = new CustomPin
             {
                 pin = new Pin
                 {
                     Type = PinType.Place,
                     Label = "Bernardinu sodas",
                     Position = new Position(54.684914, 25.293809),
                     Address = "Barboros Radvilaites g 6B, Vilnius",
                 },
                 Id = "1237474",
                 PinName = "Bernardinu sodas",
                 PinAddress = "Barboros Radvilaites g 6B, Vilnius",
                 PinType = "Parkas",


              };

                 var secondWaypoint = new CustomPin
                 {
                     pin = new Pin
                     {
                         Type = PinType.Place,
                         Label = "Katedra",
                         Position = new Position(54.6858, 25.2877),
                         Address = "Barboros Radvilaites g 6B, Vilnius",
                     },
                     Id = "1237474",
                     PinName = "Katedra",
                     PinAddress = "Sventaragio g. , Vilnius",
                     PinType = "Pastatas",
                 };

             customMap.RouteCoordinates.Clear();

             foreach (var item in listPosition)
                 {
                     customMap.RouteCoordinates.Add(new Position(item.Latitude, item.Longitude));
                 }

                 customMap.CustomPins = new List<CustomPin> { firstWaypoint, secondWaypoint };


             map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(54.684914, 25.293809), Distance.FromMeters(5000)));
                // Content = customMap;


         }
        */

        /*  private void DrawPolygon()
        {
           Polygon polygon = new Polygon
           {
               StrokeWidth = 8,
               StrokeColor = Color.FromHex("#1BA1E2"),
               FillColor = Color.FromHex("#881BA1E2"),
               Geopath =
             {
                 new Position(54.685559,25.287979),
                 new Position(54.684861,25.293494),

             }
           };

           // add the polygon to the map's MapElements collection
           map.MapElements.Add(polygon);



        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

           var wpLocations = await aboutViewModel.LoadWaypoints();


           if (wpLocations != null)
           {
               foreach (var item in wpLocations)
               {
                   Pin WaypointPins = new Pin()
                   {
                       Type = PinType.Place,
                       Label = "Waypoint Pin",
                       Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude))
                   };
                   map.Pins.Add(WaypointPins);

               }
               aboutViewModel.WaypointLocations firstWp = wpLocations[0];

               map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(firstWp.Latitude), Convert.ToDouble(firstWp.Longitude)), Distance.FromMeters(5000)));

           }
        }

        async void Map_MapClicked(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
           await DisplayAlert("Coordinates", $"Lat: { e.Position.Latitude}, Long: {e.Position.Longitude }", "ok");


           var addresses = await
            _geocoder.GetAddressesForPositionAsync(e.Position);
           await DisplayAlert("Address", addresses.FirstOrDefault()?.ToString(), "OK");


        }

        */

    }
}

