using Entities.Data_Transfer_Objects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TypeExtensions
{
    public static class CompletionExtensions
    {
        public static RouteCompletionDTO ToDTO(this RouteCompletion routeCompletion)
        {
            return new RouteCompletionDTO
            {
                RouteId = routeCompletion.RouteId,
                UserId = routeCompletion.UserId,
                LastVisit = routeCompletion.LastVisit,
                Points = routeCompletion.Points
            };
        }

        public static RouteCompletion ToEFModel(this RouteCompletionDTO routeCompletionDTO)
        {
            return new RouteCompletion
            {
                RouteId = routeCompletionDTO.RouteId,
                UserId = routeCompletionDTO.UserId,
                LastVisit = routeCompletionDTO.LastVisit,
                Points = routeCompletionDTO.Points
            };
        }

        public static WaypointCompletionDTO ToDTO(this WaypointCompletion waypointCompletion)
        {
            return new WaypointCompletionDTO
            {
                RouteCompletionRouteId = waypointCompletion.RouteCompletionRouteId,
                RouteCompletionUserId = waypointCompletion.RouteCompletionUserId,
                WaypointId = waypointCompletion.WaypointId,
                Points = waypointCompletion.Points
            };
        }

        public static WaypointCompletion ToEFModel(this WaypointCompletionDTO waypointCompletionDTO)
        {
            return new WaypointCompletion
            {
                RouteCompletionRouteId = waypointCompletionDTO.RouteCompletionRouteId,
                RouteCompletionUserId = waypointCompletionDTO.RouteCompletionUserId,
                WaypointId = waypointCompletionDTO.WaypointId,
                Points = waypointCompletionDTO.Points
            };
        }
    }
}
