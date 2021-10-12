using Encounter.Stores;
using Encounter.ViewModels;

namespace Encounter.Commands
{
    class CreateNewWaypointCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        private WaypointStore _waypointStore;

        public CreateNewWaypointCommand(RouteViewModel routeViewModel, WaypointStore waypointStore)
        {
            _routeViewModel = routeViewModel;
            _waypointStore = waypointStore;
        }

        public override void Execute(object parameter)
        {
            var waypoint = new WaypointViewModel(_waypointStore);
            _routeViewModel.AddWaypoint(waypoint);

            waypoint.Index = _routeViewModel.GetWaypointsCount();
            waypoint.Name = "Name";
            waypoint.Coordinates = new Coordinates(0.00, 0.00);

            _routeViewModel.HideEditor();
        }
    }
}
