using DataLibrary;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(WaypointId), nameof(WaypointId))]
    class WaypointViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;

        private long _waypointId;

        private long routeId;
        private int position;
        private double longitude;
        private double latitude;
        private string name;
        private string description;
        private DateTime openingHours;
        private DateTime closingHours;
        private string phoneNumber;
        private decimal price;
        private WaypointType type;

        public long Id { get; set; }
        public WaypointViewModel()
        {
            _encounterProcessor = EncounterProcessor.Instanse;
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

        public long WaypointId
        {
            get => _waypointId;
            set
            {
                _waypointId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(long waypointId)
        {
            try
            {
                var item = await _encounterProcessor.GetWaypoint(waypointId);
                Id = item.Id;
                RouteId = item.RouteId;
                Position = item.Position;
                Longitude = item.Longitude;
                Latitude = item.Latitude;
                Name = item.Name;
                Description = item.Description;
                OpeningHours = item.OpeningHours;
                ClosingHours = item.ClosingTime;
                PhoneNumber = item.PhoneNumber;
                Price = item.Price;
                Type = item.Type;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Waypoint");
            }
        }
    }
}
