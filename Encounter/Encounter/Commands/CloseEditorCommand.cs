using Encounter.ViewModels;

namespace Encounter.Commands
{
    public class CloseEditorCommand : CommandBase
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
