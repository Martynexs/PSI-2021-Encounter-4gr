using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
