using DataLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PSI.ViewModels
{
    class WaypointViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
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
        private decimal price;
        private WaypointType type;

        public long Id { get; set; }

        public long RoutesId { get; set; }
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
            get
            {
                return routeId;
            }
            set
            {
                routeId = value;
                LoadItemId(value.ToString());
            }
        }

        public long WaypointId
        {
            get
            {
                return waypointId;
            }
            set
            {
                waypointId = value;
                LoadItemId(value.ToString());
            }
        }

        public async void LoadItemId(string routeId)
        {
            try
            {
                var item = await _encounterProcessor.GetRoute(Id);
                Id = item.Id;
                RoutesId = item.CreatorId;
                Name = item.Name;
                Description = item.Description;
                //Location = item.Location;
                //Rating = item.Rating;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
