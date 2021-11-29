using DataLibrary;
using PSI.Views;
using PSI.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;
using Map3;
using Map3.Views;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(WaypointId), nameof(WaypointId))]
    class WaypointDetailViewModel : BaseViewModel
    {
        private readonly EncounterProcessor _encounterProcessor;

        public Command EditCommand { get; }

        public Command WaypointDeleteCommand { get; }

        private long waypointId;
        private long routeId;
        private int position;
        private double longitude;
        private double latitude;
        private string name;
        private string description;
        private DateTime openingHours;
        private DateTime closingHours;
        private string phoneNumber;
        private string picture;
        private decimal price;
        private WaypointType type;
        private string coordinatesText;

        public long Id { get; set; }

        public static Map map;
        private readonly MapService mapService = new MapService();
        public WaypointDetailViewModel()
        {
            _encounterProcessor = EncounterProcessor.Instanse;
            EditCommand = new Command(OnEdit);
            WaypointDeleteCommand = new Command(OnWaypointDeleteClicked);
        }

        public string CoordinatesText
        {
            get => coordinatesText;
            set => SetProperty(ref coordinatesText, value);
        }

        public int Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public double Longitude
        {
            get => longitude;
            set => SetProperty(ref longitude, value);
        }

        public double Latitude
        {
            get => latitude;
            set => SetProperty(ref latitude, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DateTime OpeningHours
        {
            get => openingHours;
            set => SetProperty(ref openingHours, value);
        }
        public DateTime ClosingHours
        {
            get => closingHours;
            set => SetProperty(ref closingHours, value);
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public WaypointType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public long RouteId
        {
            get => routeId;
            set => SetProperty(ref routeId, value);
        }

        public string Picture
        {
            get => picture;
            set => SetProperty(ref picture, value);
        }

        public long WaypointId
        {
            get => waypointId;
            set
            {
                waypointId = value;
                LoadItemId(value);
            }
        }
        private async void OnEdit()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditWaypointPopup)}?{nameof(EditWaypointViewModel.WaypointId)}={waypointId}");
        }

        private async void OnWaypointDeleteClicked(object obj)
        {
            await _encounterProcessor.DeleteWaypoint(waypointId);
            await Shell.Current.GoToAsync("..");
        }
        public async void LoadItemId(long waypointId)
        {
            try
            {
                var waypoint = await _encounterProcessor.GetWaypoint(waypointId);
                Id = waypoint.Id;
                RouteId = waypoint.RouteId;
                Position = waypoint.Position;
                Longitude = waypoint.Longitude;
                Latitude = waypoint.Latitude;
                Name = waypoint.Name;
                Description = waypoint.Description;
                OpeningHours = waypoint.OpeningHours;
                ClosingHours = waypoint.ClosingTime;
                PhoneNumber = waypoint.PhoneNumber;
                Price = waypoint.Price;
                Picture = waypoint.PictureURL;
                Type = waypoint.Type;

                CoordinatesText = await mapService.GetAddressByCoordinates(new LatLong()
                {
                    Lat = Latitude,
                    Long = Longitude
                });
                mapService.ResetSingularPin(map, true, new Position(Latitude, Longitude));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Waypoint");
            }
        }
    }
}
