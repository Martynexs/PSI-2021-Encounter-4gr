using Encounter.Commands;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    [Serializable]
    class RouteViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand CreateNewWaypoint { get; }

        public ICommand SaveRoute { get; }

        private ObservableCollection<FrameworkElement> _waypointPanels;
        public ObservableCollection<FrameworkElement> WaypointPanels => _waypointPanels;

        public List<WaypointViewModel> _waypoints;

        public WaypointEditorViewModel WaypointEditorViewModel { get; }

        private WaypointStore _waypointStore = new WaypointStore();

        public RouteViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            CreateNewWaypoint = new CreateNewWaypointCommand(this, _waypointStore);
            WaypointEditorViewModel = new WaypointEditorViewModel(_waypointStore);
            SaveRoute = new SaveRoute(this);
            _waypoints = new List<WaypointViewModel>();
            _waypointPanels = new ObservableCollection<FrameworkElement>();
        }

        public void AddWaypoint(WaypointViewModel waypoint)
        {
            _waypoints.Add(waypoint);
            WaypointPanels.Add(waypoint.GetWaypointPanel());
        }

        public int GetWaypointsCount()
        {
            return _waypoints.Count;
        }

    }
}
