using Encounter.Stores;

namespace Encounter.Commands
{
    public class SelectWaypointCommand : CommandBase
    {
        private WaypointStore _waypointStore;
        private WaypointViewModel _waypoint;

        public SelectWaypointCommand(WaypointStore waypointStore, WaypointViewModel waypointViewModel)
        {
            _waypointStore = waypointStore;
            _waypoint = waypointViewModel;
        }

        public override void Execute(object parameter)
        {
            _waypointStore.SelectedWaypoint = _waypoint;
        }
    }
}
