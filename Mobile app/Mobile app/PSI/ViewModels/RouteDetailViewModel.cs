using DataLibrary;
using PSI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RouteId), nameof(RouteId))]

    public class RouteDetailViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command RouteEditCommand { get; }

        private long routeId;
        private string name;
        private string description;
        private string location;
        private double rating;

        public long Id { get; set; }
        public RouteDetailViewModel()
        {
            EditCommand = new Command(OnEdit);
            _encounterProcessor = EncounterProcessor.Instanse;
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
        public async void LoadItemId(string routeId)
        {
            try
            {
                var route = await _encounterProcessor.GetRoute(long.Parse(routeId));
                Id = route.Id;
                CreatorId = route.CreatorId;
                Name = route.Name;
                Description = route.Description;
                Location = route.Location;
                Rating = route.Rating;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
