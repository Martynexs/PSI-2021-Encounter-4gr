using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter
{
    class WaypointController
    {
        List<VisualWaypoint> _visualWaypoints;
        Route _route;
        public WaypointController()
        {
            _visualWaypoints = new List<VisualWaypoint>();
            _route = new Route();
        }

        public int CreateNewWaypoint()
        {
            var wp = _route.CreateNewWaypoint();
            var visualWp = new VisualWaypoint(wp);
            _visualWaypoints.Add(visualWp);
            var wpnr = wp.Number;
            return wp.Number;
        }

        public void DeleteWaypoint(int index)
        {
            if (index <= _visualWaypoints.Count)
            {
                _route.RemoveWaypoint(index - 1);
                for (int i = index; i < _visualWaypoints.Count; i++)
                {
                    _visualWaypoints[i].Update();
                }
                _visualWaypoints.RemoveAt(index - 1);
            }
        }

        public void UpdateWaypoint(int number, string name, string coordinates, string type, string price, string opening, string closing, string descriptions, int index)
        {
            _route.GetWaypoint(number - 1).Update(number, name, coordinates, type, price, opening, closing, descriptions, index);
            _visualWaypoints[number - 1].Update();

            if (index != number)
            {
                ChnageWaypointPossition(number - 1, index - 1);
            }

        }

        public void ChnageWaypointPossition(int index, int newIndex)
        {
            _route.ChangeWaypointIndex(index, newIndex);

            if (newIndex < index)
            {
                for (int i = newIndex; i <= index; i++)
                {
                    _visualWaypoints[i].Update();
                }
            }
            else
            {
                for (int i = index; i <= newIndex; i++)
                {
                    _visualWaypoints[i].Update();
                }
            }

            var tempVW = _visualWaypoints[index];
            _visualWaypoints.RemoveAt(index);
            _visualWaypoints.Insert(newIndex, tempVW);
        }

        public Waypoint GetWaypoint(int index)
        {
            return _route.GetWaypoint(index - 1);
        }

        public VisualWaypoint GetVisualWaypoint(int index)
        {
            if (_visualWaypoints.Count >= index) return _visualWaypoints[index - 1];
            return null;
        }

    }
}
