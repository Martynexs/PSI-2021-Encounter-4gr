using Encounter.Stores;
using Encounter.ViewModels;

namespace Encounter.Commands
{
    class DeleteWaypointCommand : CommandBase
    {
        private WaypointStore _waypointStore;
        private RouteViewModel _routeViewModel;
        private WaypointEditorViewModel _editor;

        public DeleteWaypointCommand(WaypointStore waypointStore, RouteViewModel route, WaypointEditorViewModel editor)
        {
            _waypointStore = waypointStore;
            _routeViewModel = route;
            _editor = editor;
        }

        public override void Execute(object parameter)
        {
            _editor.EditorVisibility = System.Windows.Visibility.Hidden;
            _routeViewModel.DeleteWaypoint(_waypointStore.SelectedWaypoint.Index - 1);
        }
    }
}
