using DataLibrary;
using Map3.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PSI.Models;

namespace PSI.Services
{
    public class WaypointsCoordinatesService
    {
        private readonly EncounterProcessor _api;

        public WaypointsCoordinatesService()
        {
            _api = EncounterProcessor.Instanse;
        }

        internal async Task <List<LatLong>> LoadWaypoints()
        {
             List<LatLong> waypointLocations = new List<LatLong>
            {
                new LatLong{Lat = 54.684466, Long = 25.295119},
                new LatLong{Lat = 54.6922981, Long = 25.2798929},
                new LatLong{Lat = 54.69491220369778, Long = 25.288504312251227 },
                new LatLong {Lat = 54.69085533276373,Long =  25.26146071847952},
                new LatLong {Lat = 54.71507648287981,Long = 25.27312025175179 },
                new LatLong {Lat = 54.71203594045698,Long =  25.318506747191744 },

            };
            return waypointLocations;
        }

        internal async Task<List<VisualWaypoint>> LoadWaypointsFromAPI()
        {
            List<VisualWaypoint> result = new List<VisualWaypoint>();
            List<DataLibrary.Models.Waypoint> waypoints = await _api.GetWaypoints(1);
            foreach (DataLibrary.Models.Waypoint wp in waypoints)
            {
                VisualWaypoint ll = new VisualWaypoint();
                ll.Lat = wp.Latitude;
                ll.Long = wp.Longitude;
                ll.Name = wp.Name;
                ll.Description = wp.Description;
                result.Add(ll);
            }
            return result;
        }
    }
}
