using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {

        private EncounterProcessor _encounterProcessor;
        private Session _session;

        private long id;
        private long creatorId;
        private string name;
        private string description;
        private string location;
        private double rating;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _encounterProcessor = EncounterProcessor.Instanse;
            _session = Session.Instanse;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
        }

        public long Id
        {
            get => id;
            set => SetProperty(ref id, value);
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

        public long CreatorId
        {
            get => creatorId;
            set => SetProperty(ref creatorId, value);
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
            Route newItem = new Route()
            {
                CreatorId = 1,
                Name = Name,
                Description = Description
            };

            await _encounterProcessor.CreateRoute(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
