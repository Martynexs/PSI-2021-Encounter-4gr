using DataLibrary;
using PSI.Views;
using System;
using System.Diagnostics;
using DataLibrary.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RouteId), nameof(RouteId))]

    public class RouteDetailViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        private Session _session;

        private List<Waypoint> _waypointList = new List<Waypoint>();
        public Command RouteEditCommand { get; }

        public Command SubmitRatingCommand { get; }

        public double TotalRating { get; set; }

        private int _userRating;
        public int UserRating 
        {
            get => _userRating;
            set => SetProperty(ref _userRating, value);
        }


        private long routeId;
        private string name;
        private string description;
        private string location;
        private double rating;
        private double distances;

        public long Id { get; set; }
        public RouteDetailViewModel()
        {
            EditCommand = new Command(OnEdit);
            SubmitRatingCommand = new Command(SubmitRating);
            _encounterProcessor = EncounterProcessor.Instanse;
            _session = Session.Instanse;
        }

        public long CreatorId { get; set; }

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

        public double Distances
        {
            get => distances;
            set => SetProperty(ref distances, value);
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
        public Command EditCommand { get; }

        private async void OnEdit()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RouteEditPopup)}?{nameof(EditRouteViewModel.RouteId)}={routeId}");

        }

        IOrderedEnumerable<Waypoint> objectsQueryOrderedByDistance;
        public async void Distance()
        {
           _waypointList = await _encounterProcessor.GetWaypoints(routeId);
            var request = new GeolocationRequest(GeolocationAccuracy.Default);
                var location = await Geolocation.GetLocationAsync(request);

                foreach (var pin in _waypointList)
                {
                    pin.DistanceToUser = Xamarin.Essentials.Location.CalculateDistance(location.Latitude, location.Longitude,pin.Latitude, pin.Longitude, DistanceUnits.Kilometers);
                }
            if (_waypointList.Count != 0)
            {
                objectsQueryOrderedByDistance = _waypointList.OrderBy(pin => pin.DistanceToUser);
                Distances = Math.Round(objectsQueryOrderedByDistance.FirstOrDefault().DistanceToUser,2);
            }
        }
        public async void LoadItemId(string routeId)
        {
            Distance();
            try
            {
                var route = await _encounterProcessor.GetRoute(long.Parse(routeId));
                Id = route.Id;
                CreatorId = route.CreatorId;
                Name = route.Name;
                Description = route.Description;
                Location = route.Location;
                Rating = route.Rating;


                var userRating = await _encounterProcessor.GetRating(RouteId, _session.CurrentUser.Id);
                UserRating = userRating.Value;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void SubmitRating()
        {
            try
            {
                var rating = new Rating
                {
                    UserId = _session.CurrentUser.Id,
                    RouteId = RouteId,
                    Value = UserRating
                };
                await _encounterProcessor.SubmitRating(rating);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Failed to submit rating", "Please try again", "OK");
            }
        }


    }
}
