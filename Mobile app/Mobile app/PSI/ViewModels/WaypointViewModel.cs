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
        private long routeId;
        private long waypointId;
        private string name;
        private string description;
        private string location;
        private double rating;

        public long Id { get; set; }

        public long RoutesId { get; set; }
        public WaypointViewModel()
        {
            _encounterProcessor = EncounterProcessor.Instanse;
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

        public string Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        public double Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
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
                Location = item.Location;
                Rating = item.Rating;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
