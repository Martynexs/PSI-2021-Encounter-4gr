using Encounter.IO;
using Encounter.Models;
using Encounter.Stores;
using Encounter.ViewModels;

namespace Encounter.Commands
{
    public class LoadRouteCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private LoadRouteViewModel _loadRouteViewModel;

        public LoadRouteCommand(NavigationStore navigationStore, LoadRouteViewModel loadRouteViewModel)
        {
            _navigationStore = navigationStore;
            _loadRouteViewModel = loadRouteViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_loadRouteViewModel.SelectedRoute != null)
            {
                var waypoints = DatabaseFunctions.GetWaypoints(_loadRouteViewModel.SelectedRoute);

                var viewOnly = _loadRouteViewModel.SelectedRoute.CreatorID.Equals(Session.Get_Username());

                _navigationStore.CurrentViewModel = new RouteViewModel(_navigationStore, _loadRouteViewModel.SelectedRoute, viewOnly, waypoints);
            }
        }
    }
}
