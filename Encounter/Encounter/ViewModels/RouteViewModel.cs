using Encounter.Commands;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    class RouteViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand CreateNewWaypoint { get; }
        public ICommand SaveRoute { get; }
        public ObservableCollection<FrameworkElement> WaypointPanels { get; }

        private readonly List<WaypointViewModel> _waypoints;
        public WaypointEditorViewModel WaypointEditorViewModel { get; }

        private readonly WaypointStore _waypointStore = new WaypointStore();

        public RouteViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            CreateNewWaypoint = new CreateNewWaypointCommand(this, _waypointStore);
            WaypointEditorViewModel = new WaypointEditorViewModel(_waypointStore, this);
            SaveRoute = new SaveRouteCommand(this);
            _waypoints = new List<WaypointViewModel>();
            WaypointPanels = new ObservableCollection<FrameworkElement>();
        }

        public RouteViewModel(NavigationStore navigationStore, IEnumerable<Waypoint> waypoints)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            CreateNewWaypoint = new CreateNewWaypointCommand(this, _waypointStore);
            WaypointEditorViewModel = new WaypointEditorViewModel(_waypointStore, this);
            SaveRoute = new SaveRouteCommand(this);
            _waypoints = new List<WaypointViewModel>();
            WaypointPanels = new ObservableCollection<FrameworkElement>();
            LoadRoute(waypoints);
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
                WaypointPanels.RemoveAt(index);

                MatchIndexes();
            }
        }

        public void ChangeWaypointIndex(int index, int newIndex)
        {
            var tempW = _waypoints[index];
            var tempWV = WaypointPanels[index];

            _waypoints.RemoveAt(index);
            WaypointPanels.RemoveAt(index);

            _waypoints.Insert(newIndex, tempW);
            WaypointPanels.Insert(newIndex, tempWV);

            MatchIndexes();
        }

        private void MatchIndexes()
        {
            var i = 0;
            foreach (var waypoint in _waypoints)
            {
                waypoint.Index = i + 1;
                i++;
            }
        }

        public List<Waypoint> GetWaypoints()
        {
            var listOfWaypoints = new List<Waypoint>();
            foreach (var waypoint in _waypoints)
            {
                listOfWaypoints.Add(waypoint.GetWaypoint());
            }
            return listOfWaypoints;
        }

        public void LoadRoute(IEnumerable<Waypoint> waypoints)
        {
            try
            {
                foreach (var waypointVM in from waypoint in waypoints
                                           let waypointVM = new WaypointViewModel(_waypointStore, waypoint)
                                           select waypointVM)
                {
                    _waypoints.Add(waypointVM);
                    WaypointPanels.Add(waypointVM.GetWaypointPanel());
                }
            }
            catch(NullReferenceException)
            {
                
            }
            catch (Exception)
            {

            }
        }

        public double Distance()
        {
            double DistanceValue = 0;

            for (int i = 0; i < _waypoints.Count - 1; i++)
            {
                var x1 = _waypoints[i].Coordinates.Longitude;
                var y1 = _waypoints[i].Coordinates.Latitude;
                var x2 = _waypoints[i + 1].Coordinates.Longitude;
                var y2 = _waypoints[i + 1].Coordinates.Latitude;

                DistanceValue += Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            }
            //casting to integer km narroving type conversion
            return (int)DistanceValue;
        }

    }


}
