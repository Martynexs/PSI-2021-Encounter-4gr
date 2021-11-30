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
            await RepositoryContext.SaveChangesAsync();
            await UpdateRouteCompletionPoints(waypointCompletion);
        }

        public async Task<IEnumerable<WaypointCompletion>> GetWaypointCompletions(long routeId, long userId)
        {
            return await FindByCondition(x => x.RouteCompletionRouteId == routeId && x.RouteCompletionUserId == userId).ToListAsync();
        }

        public bool WaypointCompletionExists(long routeId, long userId)
        {
            return FindByCondition(x => x.RouteCompletionRouteId == routeId && x.RouteCompletionUserId == userId).Any();
        }

        private async Task UpdateRouteCompletionPoints(WaypointCompletion waypointCompletion)
        {
            var routeCompletion = await RepositoryContext.RouteCompletions.FindAsync(waypointCompletion.RouteCompletionRouteId, waypointCompletion.RouteCompletionUserId);

            var pt = RepositoryContext.WaypointCompletions.Join(RepositoryContext.Waypoints,
                                                                cpl => cpl.WaypointId,
                                                                wp => wp.Id,
                                                                (cpl, wp) => new
                                                                {
                                                                    WaypointId = cpl.WaypointId,
                                                                    RouteId = cpl.RouteCompletionRouteId,
                                                                    UserId = cpl.RouteCompletionUserId,
                                                                    Points = wp.Points
                                                                });
            var points = (from pts in pt
                          where pts.RouteId == routeCompletion.RouteId && pts.UserId == routeCompletion.UserId
                          group pts by pts.RouteId into grp
                          select new
                          {
                              Sum = grp.Sum(x => x.Points)
                          }).First().Sum;

            routeCompletion.Points = points;
        }


    }
}
