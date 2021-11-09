using DataLibrary;
using PSI.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(RoutesId), nameof(RoutesId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private long routeId;
        private long creatorId;
        private string name;
        private string description;
        private string location;
        private double rating;

        public long RouteId { get; set; }
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

        public long RoutesId
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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await EncounterProcessor.GetRoute(long.Parse(itemId));
                RouteId = item.Id;
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
