using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Maps;

namespace Map3.ViewModels
{
    public class aboutViewModel : BaseViewModel
    {
        public aboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }

        public class WaypointLocations
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }

        }
            internal async Task<List<WaypointLocations>> LoadWaypoints()
            {
             
            List<WaypointLocations> waypointLocations = new List<WaypointLocations>
                {
                    new WaypointLocations { Latitude = "54.684914", Longitude = "25.293809"},

                    new WaypointLocations { Latitude = "54.6858", Longitude = "25.2877"},
                };
                return waypointLocations;
            }

        private Command getRouteCommand;

        public ICommand GetRouteCommand
        {
            get
            {
                if (getRouteCommand == null)
                {
                    getRouteCommand = new Command(GetRoute);
                }

                return getRouteCommand;
            }
        }

        private void GetRoute()
        {
        }

    }
}