using DataLibrary;
using DataLibrary.Models;
using System;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RoutesId), nameof(RoutesId))]
    class NewWaypointViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;

        private long id;
        private string name;
        private string picture;
        private string description;

        public NewWaypointViewModel()
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
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Picture
        {
            get => picture;
            set => SetProperty(ref picture, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public long RoutesId
        {
            get => id;
            set => id = value;
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Waypoint newWaypoint = new Waypoint()
            {
                RouteId = id,
                Name = Name,
                Description = Description,
                PictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png"

            };

            await _encounterProcessor.CreateWaypoint(newWaypoint);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
