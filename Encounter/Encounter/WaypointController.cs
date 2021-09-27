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
            if (index <= visualWaypoints.Count)
            {
                route.RemoveWaypoint(index - 1);
                for (int i = index; i < visualWaypoints.Count; i++)
                {
                    visualWaypoints[i].Update();
                }
                visualWaypoints.RemoveAt(index-1);
            }
        }

        public void UpdateWaypoint(int number, string name, string coordinates, string type, string price, string opening, string closing, string descriptions, int index)
        {
            route.GetWaypoint(number - 1).Update(number, name, coordinates, type, price, opening, closing, descriptions, index);
            visualWaypoints[number - 1].Update();
            
            if (index != number)
            {
                ChnageWaypointPossition(number - 1, index - 1);
            }
            
        }

        public void ChnageWaypointPossition(int index, int newIndex)
        {
            route.ChangeWaypointIndex(index, newIndex);
            
            if (newIndex < index)
            {
                for(int i=newIndex; i<=index; i++)
                {
                    visualWaypoints[i].Update();
                }
            }
            else
            {
                for(int i=index; i<=newIndex; i++)
                {
                    visualWaypoints[i].Update();
                }
            }
            
            var tempVW = visualWaypoints[index];
            visualWaypoints.RemoveAt(index);
            visualWaypoints.Insert(newIndex, tempVW);
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
