using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands.AboutRoute
{
    class CloseAboutCommand : CommandBase
    {
            private WaypointEditorViewModel _waypointEditorViewModel;

            public CloseAboutCommand(WaypointEditorViewModel waypointEditorViewModel)
            {
                _waypointEditorViewModel = waypointEditorViewModel;
            }

            public override void Execute(object parameter)
            {
                _waypointEditorViewModel.AboutVisibility = System.Windows.Visibility.Hidden;
            }
    }
}
