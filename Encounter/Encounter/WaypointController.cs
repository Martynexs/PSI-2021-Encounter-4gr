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
            visualWaypoints.RemoveAt(index);
            route.RemoveWaypoint(index);
        }

        public void UpdateWaypoint()
        {

        }

        public Waypoint GetWaypoint(int index)
        {
            return route.GetWaypoint(index-1);
        }

        public VisualWaypoint GetVisualWaypoint(int index)
        {
            return visualWaypoints[index-1];
        }







    }
}
