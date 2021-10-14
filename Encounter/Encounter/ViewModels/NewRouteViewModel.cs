using Encounter.Commands;
using Encounter.Models;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class NewRouteViewModel : ViewModelBase
    {
        public ICommand NewRouteCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        NavigationStore _navigationStore;

        private readonly Route _route;
        public string Name
        {
            get => _route.Name;
            set => _route.Name = value;
        }

        public NewRouteViewModel(NavigationStore navigation)
        {
            _route = new Route();
            _navigationStore = navigation;
            NewRouteCommand = new CreateNewRouteCommand(_route, navigation);
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(_navigationStore, () => new HomeViewModel(_navigationStore));
        }



    }
}
