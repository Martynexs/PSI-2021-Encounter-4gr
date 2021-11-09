//using DataLibrary;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;
//using Xamarin.Forms;

//namespace PSI.ViewModels
//{

//    [QueryProperty(nameof(WaypointId), nameof(WaypointId))]
//    class WaypointViewModel : BaseViewModel
//    {
//        private long waypointId;
//        private long routeId;
//        private int position;
//        private double longitude;
//        private double latitude;
//        private string name;
//        private string description;
//        private DateTime openingHours;
//        private DateTime closingHours;
//        private string phone;
//        private decimal price;
//        private WaypointType type;
//        public long WaypointId { get; set; }
//        public long RouteId { get; set; }
//        public int Position
//        {
//            get => position;
//            set => SetProperty(ref position, value);
//        }
//        public double Longitude
//        {
//            get => longitude;
//            set => SetProperty(ref longitude, value);
//        }
//        public double Latitude
//        {
//            get => latitude;
//            set => SetProperty(ref latitude, value);
//        }

//        public string Name
//        {
//            get => name;
//            set => SetProperty(ref name, value);
//        }

//        public string Description
//        {
//            get => description;
//            set => SetProperty(ref description, value);
//        }

//        public DateTime Opening
//        {
//            get => openingHours;
//            set => SetProperty(ref openingHours, value);
//        }

//        public DateTime Closing
//        {
//            get => closingHours;
//            set => SetProperty(ref closingHours, value);
//        }

//        public string Phone
//        {
//            get => phone;
//            set => SetProperty(ref phone, value);
//        }

//        public decimal Price
//        {
//            get => price;
//            set => SetProperty(ref price, value);
//        }

//        public WaypointType Type
//        {
//            get => type;
//            set => SetProperty(ref type, value);
//        }

//        public long WaypointId
//        {
//            get
//            {
//                return waypointId;
//            }
//            set
//            {
//                waypointId = value;
//                LoadItemId(value.ToString());
//            }
//        }

//        public async void LoadItemId(string itemId)
//        {
//            try
//            {
//                var item = await EncounterProcessor.GetWaypoints(long.Parse(itemId));
//                WaypointId = item.Id;
//                CreatorId = item.CreatorId;
//                Name = item.Name;
//                Description = item.Description;
//                Location = item.Location;
//                Rating = item.Rating;
//            }
//            catch (Exception)
//            {
//                Debug.WriteLine("Failed to Load Item");
//            }
//        }
//    }
//}
