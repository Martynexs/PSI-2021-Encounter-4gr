using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Encounter.Commands.AboutRoute
{
    class AboutButtonCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        private WaypointEditorViewModel _waypointEditorViewModel;

        public AboutButtonCommand(WaypointEditorViewModel waypointEditorViewModel, RouteViewModel routeViewModel)
        {
            _waypointEditorViewModel = waypointEditorViewModel;
            _routeViewModel = routeViewModel;
        }

        public override void Execute(object parameter)
        {
            _waypointEditorViewModel.AboutVisibility = System.Windows.Visibility.Visible;
        }
    }
}
