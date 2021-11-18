using DataLibrary;
using PSI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RouteId), nameof(RouteId))]
    class EditRouteViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private long routeId;
        private string name;
        private string description;
        private string location;
        private double rating;

        public long Id { get; set; }

        public EditRouteViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _encounterProcessor = EncounterProcessor.Instanse;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
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

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            try
            {
                var route = await _encounterProcessor.GetRoute(long.Parse(routeId.ToString()));
                route.Id = Id;
                route.CreatorId = CreatorId;
                route.Name = Name;
                route.Description = Description;
                route.Location = Location;
                route.Rating = Rating;
                await _encounterProcessor.UpdateRoute(Id, route);
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
