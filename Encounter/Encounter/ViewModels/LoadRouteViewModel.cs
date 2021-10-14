using Encounter.Commands;
using Encounter.IO;
using Encounter.Models;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class LoadRouteViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;

        public ICommand LoadRouteCommand { get; }

        public ObservableCollection<Route> Routes { get; }

        public Route SelectedRoute { get; set; }

        public LoadRouteViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            Routes = DatabaseFunctions.GetRoutes();
            LoadRouteCommand = new LoadRouteCommand(navigationStore, this);
        }





    }
}
