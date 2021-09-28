using System;
using System.Collections.Generic;

namespace Encounter
{
    public class Route 
    {
        public List<Waypoint> waypoints;
        public double firstX;
        public double firstY;
        public double lastX;
        public double lastY;

        public Route()
        {
            waypoints = new List<Waypoint>();
        }

        public void RemoveWaypoint(int index)
        {
            if (index < waypoints.Count)
            {
                for (int i = index; i < waypoints.Count; i++)
                {
                    waypoints[i].Number -= 1;
                }
                waypoints.RemoveAt(index);
            }
        }

        public int GetWaypointsCount()
        {
           return waypoints.Count;
        }

        public Waypoint CreateNewWaypoint()
        {
            var wp = new Waypoint();
            waypoints.Add(wp);
            wp.Number = waypoints.Count;
            wp.Name = "Name";
            wp.Coordinates = "0.00, 0.00";
            return wp;
        }

        public Waypoint GetWaypoint(int index)
        {
            return index <= waypoints.Count ? waypoints[index] : null;
        }

        public void ChangeWaypointIndex(int index, int newIndex)
        {
            if (newIndex < index)
            {
                for (int i = newIndex; i <= index; i++)
                {
                    waypoints[i].Number += 1;
                }
            }
            else
            {
                for (int i = index+1; i <= newIndex; i++)
                {
                    waypoints[i].Number -= 1;
                }
            }
            var tempW = waypoints[index];
            tempW.Number = newIndex+1;
            waypoints.RemoveAt(index);
            waypoints.Insert(newIndex, tempW);
        }

    }
}
