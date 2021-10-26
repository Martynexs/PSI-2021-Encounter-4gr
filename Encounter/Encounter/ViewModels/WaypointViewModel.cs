using Encounter.Commands;
using Encounter.Stores;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Encounter
{
    public class WaypointViewModel : IComparable<WaypointViewModel>
    {
        private Waypoint _waypoint;
        private WaypointView _waypointView;

        ICommand SelectWaypointCommand;

        public WaypointViewModel(WaypointStore waypointStore)
        {
            SelectWaypointCommand = new SelectWaypointCommand(waypointStore, this);
            _waypoint = new Waypoint();
            _waypointView = new WaypointView(SelectWaypointCommand);
        }

        public WaypointViewModel(WaypointStore waypointStore, Waypoint waypoint)
        {
            SelectWaypointCommand = new SelectWaypointCommand(waypointStore, this);
            _waypoint = waypoint;
            _waypointView = new WaypointView(SelectWaypointCommand);

            _waypointView.Index = waypoint.Index;
            _waypointView.Name = waypoint.Name;
            _waypointView.Coordinates = waypoint.Coordinates;
            _waypointView.ChangeColor(waypoint.Type.GetColor());
        }

        public int Index
        {
            get => _waypoint.Index;
            set { _waypoint.Index = value; _waypointView.Index = value; }
        }
        
        public string Name
        {
            get => _waypoint.Name;
            set { _waypoint.Name = value; _waypointView.Name = value; }
        }

        public Coordinates Coordinates
        {
            get => _waypoint.Coordinates;
            set { _waypoint.Coordinates = value; _waypointView.Coordinates = value; }
        }

        public WaypointType Type
        {
            get => _waypoint.Type;
            set { _waypoint.Type = value; _waypointView.ChangeColor(value.GetColor()); }
        }

        public decimal Price
        {
            get => _waypoint.Price;
            set => _waypoint.Price = value;
        }

        public DateTime OpeningHours
        {
            get => _waypoint.OpeningHours;
            set => _waypoint.OpeningHours = value;
        }

        public DateTime ClosingTime
        {
            get => _waypoint.ClosingTime;
            set => _waypoint.ClosingTime = value;
        }
        public string PhoneNumber
        {
            get => _waypoint.PhoneNumber;
            set => _waypoint.PhoneNumber = value;
        }

        public string Description
        {
            get => _waypoint.Description;
            set => _waypoint.Description = value;
        }

        public StackPanel GetWaypointPanel()
        {
            return _waypointView.GetWaypointViewPanel();
        }

        public Waypoint GetWaypoint()
        {
            return _waypoint;
        }

        public WaypointView GetWaypointView()
        {
            return _waypointView;
        }

        public int CompareTo(WaypointViewModel other)
        {
            if (other == null) return 1;
            if (other._waypoint.Index > this._waypoint.Index) return -1;
            if (other._waypoint.Index < this._waypoint.Index) return 1;
            return 0;
        }
    }
}
