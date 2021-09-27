using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter
{
    class WaypointController
    {

        List<VisualWaypoint> visualWaypoints;
        Route route;
        public WaypointController()
        {
            visualWaypoints = new List<VisualWaypoint>();
            route = new Route();
        }

        public int CreateNewWaypoint()
        {
            var wp = route.CreateNewWaypoint();
            var visualWp = new VisualWaypoint(wp);
            visualWaypoints.Add(visualWp);
            var wpnr = wp.Number;
            return wp.Number;
        }

        public void DeleteWaypoint(int index)
        {
            if (index < visualWaypoints.Count)
            {
                visualWaypoints.RemoveAt(index);
                route.RemoveWaypoint(index);
            }
        }

        public void UpdateWaypoint(int number, string name, string coordinates, string type, string price, string opening, string closing, string descriptions, int index)
        {
            /*
            if (index != number)
            {
                Waypoint temp = list[index - 1];
                list[index - 1] = list[number - 1];
                list[number - 1] = temp;
            }
            */
            route.GetWaypoint(index - 1).Update(number, name, coordinates, type, price, opening, closing, descriptions, index);
            visualWaypoints[index - 1].Update();
        }

        public Waypoint GetWaypoint(int index)
        {
            return route.GetWaypoint(index-1);
        }

        public VisualWaypoint GetVisualWaypoint(int index)
        {
            if(visualWaypoints.Count >= index) return visualWaypoints[index-1];
            return null;
        }







    }
}
