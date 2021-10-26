using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using System.Linq;
using System.IO;

namespace Map3.Views
{
    public partial class AboutPage : ContentPage
    {
        private readonly Geocoder _geocoder = new Geocoder();
        public AboutPage()
        {
            InitializeComponent();
            Pin pinVilnius = new Pin()
            {
                Type = PinType.Place,
                Label = "Bernardinu sodas",
                Address = "Barboros Radvilaites g 6B, Vilnius",
                Position = new Position(54.684914,25.293809),
        };
            map.Pins.Add(pinVilnius);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinVilnius.Position, Distance.FromMeters(5000)));

        }
       
       async void Map_MapClicked(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
             await DisplayAlert("Coordinates", $"Lat: { e.Position.Latitude}, Long: {e.Position.Longitude }", "ok");

           
                var addresses = await
                 _geocoder.GetAddressesForPositionAsync(e.Position);
                await DisplayAlert("Address", addresses.FirstOrDefault()?.ToString(), "OK");
    

        }
    }
}