using DataLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(WaypointId), nameof(WaypointId))]
    class EditWaypointViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

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
        public EditWaypointViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _encounterProcessor = EncounterProcessor.Instance;
            _encounterProcessor = EncounterProcessor.Instance;
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
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
            get => waypointId;
            set
            {
                waypointId = value;
                LoadItemId(value);
            }
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
                Type = waypoint.Type;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Waypoint");
            }
        }

        public List<string> objectTypeNames
        {
            get
            {
                return Enum.GetNames(typeof(WaypointType)).Select(b => b).ToList();
            }
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            try
            {
                var waypoint = await _encounterProcessor.GetWaypoint(waypointId);
                waypoint.Id = Id;
                waypoint.RouteId = RouteId;
                waypoint.Position = Position;
                waypoint.Longitude = Longitude;
                waypoint.Latitude = Latitude;
                waypoint.Name = Name;
                waypoint.Description = Description;
                waypoint.OpeningHours = OpeningHours;
                waypoint.ClosingTime = ClosingHours;
                waypoint.PhoneNumber = PhoneNumber;
                waypoint.Price = Price;
                waypoint.Type = Type;
                await _encounterProcessor.UpdateWaypoint(Id, waypoint);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
