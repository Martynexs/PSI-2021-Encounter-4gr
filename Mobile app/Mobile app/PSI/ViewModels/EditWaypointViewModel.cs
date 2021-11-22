using DataLibrary;
using PSI.Services;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(WaypointId), nameof(WaypointId))]
    class EditWaypointViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command MapSearchCommand { get; }

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

        private string mapSearch;
        public static Map map;
        private MapService mapService = new MapService();

        public long Id { get; set; }

        public EditWaypointViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            MapSearchCommand = new Command(onMapSearch);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _encounterProcessor = EncounterProcessor.Instance;
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(description);
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

        public string MapSearch
        {
            get => mapSearch;
            set => SetProperty(ref mapSearch, value);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
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
                mapService.ResetSingularPin(map, true, new Position(Latitude, Longitude));
                MapSearch = "";
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

        private async void onMapSearch()
        {
            if (MapSearch == null || MapSearch.Length == 0)
            {
                await DisplayAlert("Error", "Nothing to search, please enter search query", "ok");
                return;
            }

            LatLong result = await mapService.GetCoordinatesByAddress(MapSearch);
            if (result == null)
            {
                await DisplayAlert("Error", "Nothing could be found by your search query", "ok");
                return;
            }

            Latitude = result.Lat;
            Longitude = result.Long;
            mapService.ResetSingularPin(map, true, new Position(Latitude, Longitude));
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

        public void LocationSelected(MapClickedEventArgs e)
        {
            if (e != null && e.Position != null)
            {
                Longitude = e.Position.Longitude;
                Latitude = e.Position.Latitude;
                mapService.ResetSingularPin(map, false, new Position(Latitude, Longitude));
            }
        }
    }
}
