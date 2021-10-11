using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands
{
    class SaveEditorCommand : CommandBase
    {
        private WaypointEditorViewModel _waypointEditorViewModel;
        public SaveEditorCommand(WaypointEditorViewModel waypointEditorViewModel)
        {
            _waypointEditorViewModel = waypointEditorViewModel;
        }
        public override void Execute(object parameter)
        {
            var waypoint = _waypointEditorViewModel.SelectedWaypoint;
            waypoint.Index = _waypointEditorViewModel.Index;
            waypoint.Name = _waypointEditorViewModel.Name;
            waypoint.Coordinates = _waypointEditorViewModel.Coordinates;
            //waypoint.Type = _waypointEditorViewModel.Type;
            waypoint.Price = _waypointEditorViewModel.Price;
            waypoint.OpeningHours = _waypointEditorViewModel.OpeningHours;
            waypoint.ClosingTime = _waypointEditorViewModel.ClosingTime;
            waypoint.Description = _waypointEditorViewModel.Description;
        }
    }
}