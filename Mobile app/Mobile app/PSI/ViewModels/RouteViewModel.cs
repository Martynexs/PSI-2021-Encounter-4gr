using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RouteId), nameof(RouteId))]

    public class ItemDetailViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command RouteEditCommand { get; }

        private long routeId;
        private long creatorId;
        private string name;
        private string description;
        private string location;
        private double rating;

        public long Id { get; set; }
        public ItemDetailViewModel()
        {
            EditCommand = new Command(OnEdit);
            //UpdateCommand = new Command(OnUpdate);
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

        public Command UpdateCommand { get; }

        private async void OnEdit()
        {
            // This will pop the current page off the navigation stack
            LoadPopupId(RouteId.ToString());
            await PopupNavigation.Instance.PushAsync(new RouteEditPopup());
        }

        private async void OnUpdate(string routeId)
        {
            try
            {
                var item = await _encounterProcessor.GetRoute(long.Parse(routeId));
                item.Id = Id;
                item.CreatorId = CreatorId;
                item.Name = Name;
                item.Description = Description;
                item.Location = Location;
                item.Rating = Rating;
                await _encounterProcessor.UpdateRoute(Id, item);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

            // This will pop the current page off the navigation stack
            await PopupNavigation.Instance.PopAsync();
        }
        public async void LoadItemId(string routeId)
        {
            try
            {
                var item = await _encounterProcessor.GetRoute(long.Parse(routeId));
                Id = item.Id;
                CreatorId = item.CreatorId;
                Name = item.Name;
                Description = item.Description;
                Location = item.Location;
                Rating = item.Rating;
               // await PopupNavigation.Instance.PushAsync(new RouteEditPopup(item));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            }
        public async void LoadPopupId(string routeId)
        {
            try
            {
                var item = await _encounterProcessor.GetRoute(long.Parse(routeId));
                Id = item.Id;
                CreatorId = item.CreatorId;
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
