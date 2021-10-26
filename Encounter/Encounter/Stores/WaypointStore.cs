using System;

namespace Encounter.Stores
{
    public class WaypointStore
    {
        public event Action SelectedWaypointChanged;

        public WaypointViewModel _selectedWaypoint;
        public WaypointViewModel SelectedWaypoint
        {
            get => _selectedWaypoint;
            set
            {
                _selectedWaypoint = value;
                OnSelectedWaypointChanged();
            }
        }

        private void OnSelectedWaypointChanged()
        {
            SelectedWaypointChanged?.Invoke();
        }
    }
}
