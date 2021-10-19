using Encounter.Commands;
using Encounter.IO;
using Encounter.Models;
using Encounter.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class LoadRouteViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;

        public ICommand LoadRouteCommand { get; }

        public ICommand NavigateHomeCommand { get; }

        private List<Route> _allRoutes;

        public ObservableCollection<Route> Routes { get; set; }

        public Route SelectedRoute { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                FilterRoutes(value);
                OnPropertyChanged();
            }
        }

        public LoadRouteViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            Routes = DatabaseFunctions.GetRoutes();
            _allRoutes = Routes.ToList();
            LoadRouteCommand = new LoadRouteCommand(navigationStore, this);
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }

        private void FilterRoutes(string search)
        {
            Routes = new ObservableCollection<Route>(_allRoutes.Where(x => x.ToString().ToLower().Contains(search.ToLower())).Select(x => x).ToList());
        }
    }
}
