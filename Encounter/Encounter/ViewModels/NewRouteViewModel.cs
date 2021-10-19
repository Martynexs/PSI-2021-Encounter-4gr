using Encounter.Commands;
using Encounter.Models;
using Encounter.Stores;
using System;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class NewRouteViewModel : ViewModelBase
    {
        public ICommand NewRouteCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        private NavigationStore _navigationStore;

        private readonly Route _route;
        public string Name
        {
            get => _route.Name;
            set => _route.Name = value;
        }

        public NewRouteViewModel(NavigationStore navigation)
        {
            _route = new Route(new Random(Guid.NewGuid().GetHashCode()).Next(10000, 99999));
            _navigationStore = navigation;
            NewRouteCommand = new CreateNewRouteCommand(_route, navigation);
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(_navigationStore, () => new HomeViewModel(_navigationStore));
        }
    }
}
