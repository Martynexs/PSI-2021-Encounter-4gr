using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
