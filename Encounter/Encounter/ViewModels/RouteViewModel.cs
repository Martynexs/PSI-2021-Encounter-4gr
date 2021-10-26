using Encounter.Commands;
using Encounter.Commands.AboutRoute;
using Encounter.Commands.RouteVM;
using Encounter.Models;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class RouteViewModel : ViewModelBase
    {
        public bool ViewOnly { get; set; }
        public Visibility ViewOnlyVisibility => ViewOnly ? Visibility.Visible : Visibility.Hidden;

        public ICommand NavigateHomeCommand { get; }
        public ICommand CreateNewWaypoint { get; }
        public ICommand SaveRoute { get; }
        public ICommand DeleteRoute { get; }
        public ICommand AboutRoute { get; }

        public ObservableCollection<FrameworkElement> WaypointPanels { get; set; }
        public List<WaypointType> AllWaypointTypes { get; set; } = WayPointTypeExtensions.GetAllTypes();

        private readonly List<WaypointViewModel> _waypoints;

        public WaypointEditorViewModel WaypointEditorViewModel { get; }
        public AboutRouteViewModel AboutRouteViewModel { get; }

        private readonly WaypointStore _waypointStore = new WaypointStore();

        private bool _filteringEnabled;
        public bool FilteringEnabled 
        {
            get => _filteringEnabled; 
            set
            {
                _filteringEnabled = value;

                if (!value)
                {
                    WaypointPanels = new ObservableCollection<FrameworkElement>(_waypoints.Select(x => x.GetWaypointPanel()).ToList());
                }
                else
                {
                    FilterWaypoints();
                }

                OnPropertyChanged();
            }
        }

        private WaypointType _selectedFilter;
        public WaypointType SelectedFilter 
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                FilterWaypoints();
                OnPropertyChanged();
            }
        }

        public Route Route { get; }

        //Builders
        public RouteViewModel(NavigationStore navigationStore, Route route)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            CreateNewWaypoint = new CreateNewWaypointCommand(this, _waypointStore);

            WaypointEditorViewModel = new WaypointEditorViewModel(_waypointStore, this, true);
            AboutRouteViewModel = new AboutRouteViewModel(this, route, true);

            SaveRoute = new SaveRouteCommand(this);
            _waypoints = new List<WaypointViewModel>();
            AboutRoute = new AboutButtonCommand(AboutRouteViewModel, this);
            WaypointPanels = new ObservableCollection<FrameworkElement>();
            DeleteRoute = new DeleteRouteCommand(this, navigationStore);
            ViewOnly = true;
            Route = route;
        }

        public RouteViewModel(NavigationStore navigationStore, Route route, bool viewOnly, IEnumerable<Waypoint> waypoints)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            CreateNewWaypoint = new CreateNewWaypointCommand(this, _waypointStore);

            WaypointEditorViewModel = new WaypointEditorViewModel(_waypointStore, this, viewOnly);
            AboutRouteViewModel = new AboutRouteViewModel(this, route, viewOnly);

            SaveRoute = new SaveRouteCommand(this);
            _waypoints = new List<WaypointViewModel>();
            WaypointPanels = new ObservableCollection<FrameworkElement>();
            AboutRoute = new AboutButtonCommand(AboutRouteViewModel, this);
            DeleteRoute = new DeleteRouteCommand(this, navigationStore);
            Route = route;
            ViewOnly = viewOnly;
            LoadRoute(waypoints);
        }

        //Functions
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

        public void HideAbout()
        {
            AboutRouteViewModel.Visibility = Visibility.Hidden;
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
            var listOfWaypoints = _waypoints.Select(x => x.GetWaypoint()).ToList();
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

        private void FilterWaypoints()
        {
            WaypointPanels = new ObservableCollection<FrameworkElement>( _waypoints.Where(x => x.Type == SelectedFilter).Select(x => x.GetWaypointPanel()).ToList());
        }
    }
}
