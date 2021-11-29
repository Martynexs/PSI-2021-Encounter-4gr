using Contracts;
using EncounterAPI.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WaypointCompletionRepository : RepositoryBase<WaypointCompletion>, IWaypointCompletionRepository
    {
        public WaypointCompletionRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public async Task CreateWaypointCompletion(WaypointCompletion waypointCompletion)
        {
            Create(waypointCompletion);
            await UpdateRouteCompletionPoints(waypointCompletion);
        }

        public async Task<IEnumerable<WaypointCompletion>> GetWaypointCompletions(long routeId, long userId)
        {
            return await FindByCondition(x => x.RouteCompletionRouteId == routeId && x.RouteCompletionUserId == userId).ToListAsync();
        }

        private async Task UpdateRouteCompletionPoints(WaypointCompletion waypointCompletion)
        {
            var routeCompletion = await RepositoryContext.RouteCompletions.FindAsync(waypointCompletion.RouteCompletionRouteId, waypointCompletion.RouteCompletionUserId);
            var waypoint = await RepositoryContext.Waypoints.FindAsync(waypointCompletion.WaypointId);
            routeCompletion.Points += waypoint.Points;
        }


    }
}
