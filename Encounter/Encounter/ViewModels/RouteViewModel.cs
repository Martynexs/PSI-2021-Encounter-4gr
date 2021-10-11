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
    class RouteViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand CreateNewWaypoint { get; }

        private ObservableCollection<FrameworkElement> _waypointPanels;
        public ObservableCollection<FrameworkElement> WaypointPanels => _waypointPanels;

        private readonly List<WaypointViewModel> _waypoints;

        public WaypointEditorViewModel WaypointEditorViewModel { get; }

        private WaypointStore _waypointStore = new WaypointStore();

        public RouteViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            CreateNewWaypoint = new CreateNewWaypointCommand(this, _waypointStore);
            WaypointEditorViewModel = new WaypointEditorViewModel(_waypointStore, this);
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

        public void HideEditor()
        {
            WaypointEditorViewModel.EditorVisibility = Visibility.Hidden;
        }

        public void DeleteWaypoint(int index)
        {
            if (index < _waypoints.Count)
            {
                _waypoints.RemoveAt(index);
                _waypointPanels.RemoveAt(index);

                MatchIndexes();
            }
        }

        public void ChangeWaypointIndex(int index, int newIndex)
        {
            var tempW = _waypoints[index];
            var tempWV = _waypointPanels[index];

            _waypoints.RemoveAt(index);
            _waypointPanels.RemoveAt(index);

            _waypoints.Insert(newIndex, tempW);
            _waypointPanels.Insert(newIndex, tempWV);

            MatchIndexes();
        }

        private void MatchIndexes()
        {
            int i = 0;
            foreach (var waypoint in _waypoints)
            {
                waypoint.Index = i + 1;
                i++;
            }
        }

    }


}
