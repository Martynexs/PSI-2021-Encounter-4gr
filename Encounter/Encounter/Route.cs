using System;
using System.Collections.Generic;

namespace Encounter
{
    public class Route 
    {
        private List<WaypointViewModel> _waypoints;

        public Route()
        {
            _waypoints = new List<WaypointViewModel>();
        }

        public WaypointViewModel CreateNewWaypoint()
        {
            var waypoint = new WaypointViewModel();
            _waypoints.Add(waypoint);
            waypoint.Index = _waypoints.Count;
            waypoint.Name = "Name";
            waypoint.Coordinates = (0.00, 0.00);
            return waypoint;
        }

        public void RemoveWaypoint(int index)
        {
            if (index < _waypoints.Count)
            {
                for (int i = index; i < _waypoints.Count; i++)
                {
                    _waypoints[i].Index -= 1;
                }
                _waypoints.RemoveAt(index);
            }
        }

        public int GetWaypointsCount()
        {
           return _waypoints.Count;
        }

        public WaypointViewModel GetWaypoint(int index)
        {
            return index <= _waypoints.Count ? _waypoints[index] : null;
        }

        public void ChangeWaypointIndex(int index, int newIndex)
        {
            if (newIndex < index)
            {
                for (int i = newIndex; i <= index; i++)
                {
                    _waypoints[i].Index += 1;
                }
            }
            else
            {
                for (int i = index+1; i <= newIndex; i++)
                {
                    _waypoints[i].Index -= 1;
                }
            }
            var tempW = _waypoints[index];
            tempW.Index = newIndex+1;
            _waypoints.RemoveAt(index);
            _waypoints.Insert(newIndex, tempW);
        }
    }
}
