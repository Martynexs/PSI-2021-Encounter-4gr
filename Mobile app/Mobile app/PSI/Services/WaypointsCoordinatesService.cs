﻿using DataLibrary;
using System.Collections.Generic;
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

        public async Task<List<VisualWaypoint>> LoadWaypointsFromAPI(long routeId)
        {
            var result = new List<VisualWaypoint>();
            var waypoints = await _api.GetWaypoints(routeId);
            foreach (DataLibrary.Models.Waypoint wp in waypoints)
            {
                var ll = new VisualWaypoint
                {
                    Id = wp.Id,
                    Lat = wp.Latitude,
                    Long = wp.Longitude,
                    Name = wp.Name,
                    Description = wp.Description
                };
                result.Add(ll);
            }
            return result;
        }
    }
}
