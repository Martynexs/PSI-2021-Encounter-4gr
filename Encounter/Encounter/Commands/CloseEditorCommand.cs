using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands
{
    class CloseEditorCommand : CommandBase
    {
        private WaypointEditorViewModel _waypointEditorViewModel;

        public CloseEditorCommand(WaypointEditorViewModel waypointEditorViewModel)
        {
            _waypointEditorViewModel = waypointEditorViewModel;
        }

        public override void Execute(object parameter)
        {
            _waypointEditorViewModel.EditorVisibility = System.Windows.Visibility.Hidden;
        }
    }
}
