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

        public Route(List<Waypoint> wp)
        {
            waypoints = wp;
        }

        public void CalculateFirstAndLastCoordinates(string first_coordinate, string last_coordinate)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Waypoint w = waypoints[i];
                if (i == 0)
                {
                    first_coordinate = w.Coordinates;
                }
                if (i == waypoints.Count)
                {
                   last_coordinate = w.Coordinates;
                }
            }

            string[] first_coords = first_coordinate.Split(";");
            string firstX = first_coords[0];
            string firstY = first_coords[1];

            string[] last_coords = last_coordinate.Split(";");
            string lastX = last_coords[0];
            string lastY = last_coords[1];        

            this.firstX = double.Parse(firstX);
            this.firstY = double.Parse(firstY);

            this.lastX = double.Parse(lastX);
            this.lastY = double.Parse(lastY);
        }

       public double getDistance(double firstX, double firstY, double lastX, double lastY)
        {
            return Math.Round(Math.Sqrt(Math.Pow(lastX - firstX, 2) + Math.Pow(lastY - firstY, 2)),2);
        }

        public bool IfCanAdd()
        {
            return waypoints.Count < 100;
        }

       public void Insert(int index, Waypoint w)
        {
            waypoints.Insert(index, w);

        }
        /// <summary>
        /// Adds given waypoint to the end of waypoints list.
        /// </summary>
        /// <param name="w">Waypoint to append</param>
        public void Append(Waypoint w) //?????
        {
            waypoints.Add(w);
        }
        public void RemoveWaypoint(int index)
        {
            waypoints.RemoveAt(index);
        }
        public void RemoveByName(string name)
        {    
            int index = FindByName(name); ;
            waypoints.RemoveAt(index);
        }
        public int GetIndex(string name)
        {
            return FindByName(name);
        }

        public int GetWaypointsCount()
        {
           return waypoints.Count;
        }
        public Boolean Exists(string name)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i].Name == name)
                {
                    return true;

                }
            }
            return false;
        }
        public int FindByName(string name)
        {

            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i].Name == name)
                {
                    return i;
                }   
            }
             return -1; 
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
            waypoints[index].Number = newIndex;
        }

    }
}
