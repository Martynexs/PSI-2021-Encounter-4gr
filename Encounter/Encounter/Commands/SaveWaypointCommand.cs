using Encounter.Stores;
using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands
{
    class SaveWaypointCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        private WaypointEditorViewModel _editor;
        private WaypointStore _waypointStore;

        public SaveWaypointCommand(RouteViewModel routeViewModel, WaypointEditorViewModel editor, WaypointStore waypointStore)
        {
            _routeViewModel = routeViewModel;
            _editor = editor;
            _waypointStore = waypointStore;
        }

        public override void Execute(object parameter)
        {
            var waypoint = _waypointStore.SelectedWaypoint;

            waypoint.Name = _editor.Name;
            waypoint.Coordinates = _editor.Coordinates;
            waypoint.OpeningHours = _editor.OpeningHours;
            waypoint.ClosingTime = _editor.ClosingTime;
            waypoint.Type = _editor.Type;
            waypoint.Description = _editor.Description;
            waypoint.PhoneNumber = _editor.PhoneNumber;
            waypoint.Price = _editor.Price;

            if (waypoint.Index != _editor.Index)
            {
                _routeViewModel.ChangeWaypointIndex(waypoint.Index - 1, _editor.Index - 1);
            }
            _editor.EditorVisibility = System.Windows.Visibility.Hidden;
        }
    }
}
