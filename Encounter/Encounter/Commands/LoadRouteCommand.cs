using Encounter.Stores;
using Encounter.ViewModels;

namespace Encounter.Commands
{
    class LoadRouteCommand : CommandBase
    {
        NavigationStore _navigationStore;

        public LoadRouteCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            var waypoints = Files.Read();
            _navigationStore.CurrentViewModel = new RouteViewModel(_navigationStore, waypoints);
        }
    }
}
